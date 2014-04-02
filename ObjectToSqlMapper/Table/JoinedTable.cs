namespace ObjectToSqlMapper.Table
{
	using System;
	using System.Collections.Generic;

	using ObjectToSqlMapper.Utils;

	public class JoinedTable : Table
	{
		private const string Template = "{0} JOIN [{1}].[{2}] {3}";

		private static readonly Dictionary<JoinType, string> JoinTypeMap = new Dictionary<JoinType, string>
			                                                                   {
				                                                                   { JoinType.Full, "FULL OUTER" },
				                                                                   { JoinType.Inner, "INNER" },
				                                                                   { JoinType.LeftOuter, "LEFT OUTER" },
				                                                                   { JoinType.RightOuter, "RIGHT OUTER" },
			                                                                   };

		private readonly string joinType;

		private readonly string ruleForCombination = Environment.NewLine;

		private readonly ISqlConstituent joinedTableOn;

		public JoinedTable(
			string schema,
			string table,
			string alias,
			IAliasable parentTable,
			Column parentTableColumn,
			ForeignColumn childTableColumn,
			JoinType joinType = JoinType.LeftOuter) : base(schema, table, alias)
		{
			this.joinedTableOn = new JoinedTableOn(parentTableColumn, childTableColumn, parentTable, this);
			
			if (!JoinTypeMap.ContainsKey(joinType))
			{
				throw new InvalidOperationException("Must contain a valid join type");	
			}

			this.joinType = JoinTypeMap[joinType];
		}

		public override string Expression
		{
			get
			{
				return Template.FormatCurrentCulture(this.joinType, this.SchemaName, this.TableName, this.Alias) +
					this.ruleForCombination +
					this.joinedTableOn.Expression;
			}
		}
	}
}
