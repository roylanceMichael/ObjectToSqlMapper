namespace ObjectToSqlMapperTests
{
	using System;
	using System.Collections.Generic;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using ObjectToSqlMapper;
	using ObjectToSqlMapper.Table;

	public class SelectStatementBuilderTests
	{
		[TestClass]
		public class Build
		{
			[TestMethod]
			public void BuildSelectStringWithNoForeignTable()
			{
				// arrange
				var column = new NormalColumn("Test");
				var table = new Table("Hello", "Kitty", "hk");

				var builder = new SelectStatementBuilder(
					new List<ISqlConstituent> { column },
					new List<ISqlConstituent>(),
					table,
					new List<ISqlConstituent>());

				// act
				var selectStatement = builder.Build();

				// assert
				const string ExpectedResult = @"SELECT
	Test
FROM [Hello].[Kitty] as hk";

				Assert.AreEqual(ExpectedResult, selectStatement);
			}
		}
	}
}
