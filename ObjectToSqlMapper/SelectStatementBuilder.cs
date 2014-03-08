namespace ObjectToSqlMapper
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	using ObjectToSqlMapper.Utils;

	public class SelectStatementBuilder : IBuilder<string>
	{
		private readonly IList<ISqlConstituent> tableColumns;

		private readonly IList<ISqlConstituent> foreignTableColumns;

		private readonly ISqlConstituent table;

		private readonly IList<ISqlConstituent> foreignTables; 

		public SelectStatementBuilder(
			IList<ISqlConstituent> tableColumns,
			IList<ISqlConstituent> foreignTableColumns,
			ISqlConstituent table,
			IList<ISqlConstituent> foreignTables)
		{
			tableColumns.CheckWhetherArgumentIsNull("tableColumns");
			foreignTableColumns.CheckWhetherArgumentIsNull("foreignTableColumns");
			table.CheckWhetherArgumentIsNull("table");
			foreignTables.CheckWhetherArgumentIsNull("foreignTables");

			this.tableColumns = tableColumns;
			this.foreignTableColumns = foreignTableColumns;
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

		private IEnumerable<ISqlConstituent> AllCombinedColumns()
		{
			// get a unique list of all the column names
			var hashSet = new HashSet<ISqlConstituent>();

			foreach (var tableColumn in this.tableColumns)
			{
				yield return tableColumn;
				hashSet.Add(tableColumn);
			}

			foreach (var foreignTableColumn in this.foreignTableColumns
				.Where(foreignTableColumn => 
					hashSet.All(constituent => constituent.Expression != foreignTableColumn.Expression)))
			{
				yield return foreignTableColumn;
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
