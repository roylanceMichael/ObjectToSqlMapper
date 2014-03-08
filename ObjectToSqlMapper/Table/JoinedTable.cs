namespace ObjectToSqlMapper.Table
{
	using System;

	using ObjectToSqlMapper.Utils;

	public class JoinedTable : ISqlConstituent
	{
		private const string Template = "JOIN [{0}].[{1}] {2}";

		private readonly string ruleForCombination = Environment.NewLine;

		private readonly string tableName;

		private readonly string schemaName;

		private readonly string tableAlias;

		private readonly ISqlConstituent joinedTableOn;

		public JoinedTable(string schemaName, string tableName, string tableAlias, ISqlConstituent joinedTableOn)
		{
			this.tableName = tableName;
			this.schemaName = schemaName;
			this.tableAlias = tableAlias;
			this.joinedTableOn = joinedTableOn;
		}

		public string Expression
		{
			get
			{
				return Template.FormatCurrentCulture(this.schemaName, this.tableName, this.tableAlias) + 
					this.ruleForCombination + 
					this.joinedTableOn.Expression;
			}
		}
	}
}
