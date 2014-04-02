namespace ObjectToSqlMapper
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	using ObjectToSqlMapper.Utils;

	public class SelectStatementBuilder : IBuilder<string>
	{
		private readonly IList<ISqlConstituent> primaryColumns;
 
		private readonly IList<ISqlConstituent> normalColumns;

		private readonly IList<ISqlConstituent> otherColumns; 

		private readonly IList<ISqlConstituent> foreignColumns;

		private readonly ISqlConstituent table;

		private readonly IList<ISqlConstituent> foreignTables; 

		public SelectStatementBuilder(
			IList<ISqlConstituent> columns,
			ISqlConstituent table,
			IList<ISqlConstituent> foreignTables)
		{
			columns.CheckWhetherArgumentIsNull("columns");
			table.CheckWhetherArgumentIsNull("table");
			foreignTables.CheckWhetherArgumentIsNull("foreignTables");

			// get columns
			this.normalColumns = GetNormalColumns(columns).ToList();
			this.foreignColumns = GetForeignColumns(columns).ToList();
			this.primaryColumns = GetPrimaryColumns(columns).ToList();
			this.otherColumns = this.GetOtherColumns(columns).ToList();

			this.table = table;
			this.foreignTables = foreignTables;
		}

		public string Build()
		{
			var workspace = new StringBuilder("SELECT");
			
			workspace.AppendLine();

			foreach (var columnExpression in this.BuildColumns())
			{
				workspace.AppendLine(columnExpression);
			}

			workspace.AppendLine(this.table.Expression);

			foreach (var foreignTable in this.foreignTables)
			{
				workspace.AppendLine(foreignTable.Expression);
			}

			return workspace.ToString().Trim();
		}

		private static IEnumerable<ISqlConstituent> GetPrimaryColumns(IEnumerable<ISqlConstituent> sqlConstituents)
		{
			return sqlConstituents.OfType<PrimaryColumn>();
		}

		private static IEnumerable<ISqlConstituent> GetNormalColumns(IEnumerable<ISqlConstituent> sqlConstituents)
		{
			return sqlConstituents.OfType<NormalColumn>();
		}

		private static IEnumerable<ISqlConstituent> GetForeignColumns(IEnumerable<ISqlConstituent> sqlConstituents)
		{
			return sqlConstituents.OfType<ForeignColumn>();
		}

		private IEnumerable<ISqlConstituent> GetOtherColumns(IEnumerable<ISqlConstituent> sqlConstituents)
		{
			return sqlConstituents.Except(this.primaryColumns).Except(this.normalColumns).Except(this.foreignColumns);
		}

		private IEnumerable<ISqlConstituent> AllCombinedColumns()
		{
			// primary keys first
			foreach (var tableColumn in this.primaryColumns)
			{
				yield return tableColumn;
			}

			// all others later
			foreach (var tableColumn in this.normalColumns)
			{
				yield return tableColumn;
			}

			foreach (var foreignTableColumn in this.foreignColumns)
			{
				yield return foreignTableColumn;
			}

			foreach (var otherColumn in this.otherColumns)
			{
				yield return otherColumn;
			}
		}

		private IEnumerable<string> BuildColumns()
		{
			// ya, no need to yield in the method above
			var allColumns = this.AllCombinedColumns().ToList();

			var lastColumn = allColumns.Last();

			foreach (var column in allColumns.Where(constituent => constituent != lastColumn))
			{
				yield return column.Expression + ",";
			}

			yield return lastColumn.Expression;
		}
	}
}
