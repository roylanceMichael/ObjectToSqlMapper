namespace ObjectToSqlMapperTests
{
	using System.Collections.Generic;
	using System.Text.RegularExpressions;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using ObjectToSqlMapper;
	using ObjectToSqlMapper.Table;

	public class SelectStatementBuilderTests
	{
		private static void AssertSameCharactersWithoutSpaces(string firstValue, string secondValue)
		{
			Assert.AreEqual(
					Regex.Replace(firstValue, "\\s+", string.Empty),
					Regex.Replace(secondValue, "\\s+", string.Empty));
		}

		[TestClass]
		public class Build
		{
			[TestMethod]
			public void BuildSelectStringWithNoForeignTable()
			{
				// arrange
				var table = new Table("Hello", "Kitty", "hk");
				var column = new NormalColumn("Test", table);

				var builder = new SelectStatementBuilder(
					new List<ISqlConstituent> { column },
					new List<ISqlConstituent>(),
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
			public void BuildSelectStringWithForeignTable()
			{
				// arrange
				var table = new Table("Hello", "Kitty", "hk");
				var column = new NormalColumn("Test", table);

				var foreignColumn = new ForeignColumn("Sample");
				var joinedTable = new JoinedTable("Something", "Somewhere", "ss", table, column, foreignColumn);

				var builder = new SelectStatementBuilder(
					new List<ISqlConstituent> { column },
					new List<ISqlConstituent> { foreignColumn },
					table,
					new List<ISqlConstituent> { joinedTable });

				// act
				var selectStatement = builder.Build();

				// assert
				const string ExpectedResult = @"SELECT
	[hk].[Test],
	[ss].[Sample]
FROM [Hello].[Kitty] hk
FULL OUTER JOIN [Something].[Somewhere] ss
	ON hk.Test = ss.Sample";

				AssertSameCharactersWithoutSpaces(ExpectedResult, selectStatement);
			}
		}
	}
}
