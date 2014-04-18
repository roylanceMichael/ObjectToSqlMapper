namespace ObjectToSqlMapper.Table
{
	using ObjectToSqlMapper.Utils;

	internal class JoinedTableOn : ISqlConstituent
	{
		private const string JoinedTemplate = StringExtensions.Tab + " ON {4}{0}{5}.{4}{1}{5} = {4}{2}{5}.{4}{3}{5}";

		private readonly ISqlConstituent parentTableColumn;

		private readonly ISqlConstituent joinedTableColumn;

		private readonly IAliasable parentTableAlias;

		private readonly IAliasable joinedTableAlias;

		private readonly FormatSystemModel formatModel;

		public JoinedTableOn(Column parentTableColumn, ForeignColumn joinedTableColumn, IAliasable parentTableAlias, IAliasable joinedTableAlias, SqlType sqlType)
		{
			parentTableColumn.CheckWhetherArgumentIsNull("parentTableColumn");
			joinedTableColumn.CheckWhetherArgumentIsNull("joinedTableColumn");
			parentTableAlias.CheckWhetherArgumentIsNull("parentTableAlias");
			joinedTableAlias.CheckWhetherArgumentIsNull("joinedTableAlias");

			this.parentTableColumn = parentTableColumn;
			this.joinedTableColumn = joinedTableColumn;
			this.parentTableAlias = parentTableAlias;
			this.joinedTableAlias = joinedTableAlias;
			this.formatModel = sqlType.BuildFormatSystemModel();

			// set aliases
			joinedTableColumn.SetTableAlias(this.joinedTableAlias.Alias);
		}

		public string Name
		{
			get
			{
				return string.Empty;
			}
		}

		public string Expression
		{
			get
			{
				return JoinedTemplate.FormatCurrentCulture(
					this.parentTableAlias.Alias, 
					this.parentTableColumn.Name, 
					this.joinedTableAlias.Alias, 
					this.joinedTableColumn.Name,
					this.formatModel.Beginning,
					this.formatModel.Ending);
			}
		}
	}
}
