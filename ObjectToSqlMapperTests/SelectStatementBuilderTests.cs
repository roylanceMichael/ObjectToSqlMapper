namespace ObjectToSqlMapperTests
{
	using System.Collections.Generic;
	using System.Text.RegularExpressions;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using ObjectToSqlMapper;
	using ObjectToSqlMapper.Table;
	using ObjectToSqlMapper.Utils;

	public class SelectStatementBuilderTests
	{
		private const string WhiteSpace = "\\s+";

		private static void AssertSameCharactersWithoutSpaces(string firstValue, string secondValue)
		{
			Assert.AreEqual(
					Regex.Replace(firstValue, WhiteSpace, string.Empty),
					Regex.Replace(secondValue, WhiteSpace, string.Empty));
		}

		[TestClass]
		public class Build
		{
			[TestMethod]
			public void BuildSelectStringWithNoForeignTableSqlServer()
			{
				// arrange
				var table = new Table("Hello", "Kitty", "hk", SqlType.SqlServer);
				var column = new NormalColumn("Test", table, SqlType.SqlServer);

				var builder = new SelectStatementBuilder(
					new List<ISqlConstituent> { column },
					table,
					new List<ISqlConstituent>());

				// act
				var selectStatement = builder.Build();

				// assert
				const string ExpectedResult = @"SELECT
	[hk].[Test]
FROM [Hello].[Kitty] hk";

				AssertSameCharactersWithoutSpaces(ExpectedResult, selectStatement);
			}

			[TestMethod]
			public void BuildSelectStringWithNoForeignTableOracle()
			{
				// arrange
				var table = new Table("Hello", "Kitty", "hk", SqlType.Oracle);
				var column = new NormalColumn("Test", table, SqlType.Oracle);

				var builder = new SelectStatementBuilder(
					new List<ISqlConstituent> { column },
					table,
					new List<ISqlConstituent>());

				// act
				var selectStatement = builder.Build();

				// assert
				const string ExpectedResult = @"SELECT
	hk.Test
FROM Hello.Kitty hk";

				AssertSameCharactersWithoutSpaces(ExpectedResult, selectStatement);
			}

			[TestMethod]
			public void BuildSelectStringWithForeignTableSqlServer()
			{
				// arrange
				var table = new Table("Hello", "Kitty", "hk");
				var column = new NormalColumn("Test", table);

				var foreignColumn = new ForeignColumn("Sample");
				var joinedTable = new JoinedTable("Something", "Somewhere", "ss", table, column, foreignColumn);

				var builder = new SelectStatementBuilder(
					new List<ISqlConstituent> { column, foreignColumn },
					table,
					new List<ISqlConstituent> { joinedTable });

				// act
				var selectStatement = builder.Build();

				// assert
				const string ExpectedResult = @"SELECT
	[hk].[Test],
	[ss].[Sample]
FROM [Hello].[Kitty] hk
LEFT OUTER JOIN [Something].[Somewhere] ss
	ON [hk].[Test] = [ss].[Sample]";

				AssertSameCharactersWithoutSpaces(ExpectedResult, selectStatement);
			}

			[TestMethod]
			public void BuildSelectStringWithForeignTableOracle()
			{
				// arrange
				var table = new Table("Hello", "Kitty", "hk", SqlType.Oracle);
				var column = new NormalColumn("Test", table, SqlType.Oracle);

				var foreignColumn = new ForeignColumn("Sample", SqlType.Oracle);
				var joinedTable = new JoinedTable("Something", "Somewhere", "ss", table, column, foreignColumn, sqlType: SqlType.Oracle);

				var builder = new SelectStatementBuilder(
					new List<ISqlConstituent> { column, foreignColumn },
					table,
					new List<ISqlConstituent> { joinedTable });

				// act
				var selectStatement = builder.Build();

				// assert
				const string ExpectedResult = @"SELECT
	hk.Test,
	ss.Sample
FROM Hello.Kitty hk
LEFT OUTER JOIN Something.Somewhere ss
	ON hk.Test = ss.Sample";

				AssertSameCharactersWithoutSpaces(ExpectedResult, selectStatement);
			}
		}
	}
}
