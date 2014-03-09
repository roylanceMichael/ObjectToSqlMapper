namespace ObjectToSqlMapper.Table
{
	using System;

	using ObjectToSqlMapper.Utils;

	public class JoinedTable : Table
	{
		private const string Template = "JOIN [{0}].[{1}] {2}";

		private readonly string ruleForCombination = Environment.NewLine;

		private readonly ISqlConstituent joinedTableOn;

		public JoinedTable(
			string schema,
			string table,
			string alias,
			IAliasable parentTable,
			Column parentTableColumn,
			Column childTableColumn) : base(schema, table, alias)
		{
			this.joinedTableOn = new JoinedTableOn(parentTableColumn, childTableColumn, parentTable, this);
		}

		public override string Expression
		{
			get
			{
				return Template.FormatCurrentCulture(this.SchemaName, this.TableName, this.Alias) +
					this.ruleForCombination +
					this.joinedTableOn.Expression;
			}
		}
	}
}
