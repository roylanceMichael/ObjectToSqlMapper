namespace ObjectToSqlMapper.Table
{
	using ObjectToSqlMapper.Utils;

	public class JoinedTableOn : ISqlConstituent
	{
		private const string JoinedTemplate = StringExtensions.Tab + " ON {0}.{1} = {2}.{3}";

		private readonly string parentTableColumn;

		private readonly string joinedTableColumn;

		private readonly string parentTableAlias;

		private readonly string joinedTableAlias;

		public JoinedTableOn(string parentTableColumn, string joinedTableColumn, string parentTableAlias, string joinedTableAlias)
		{
			this.parentTableColumn = parentTableColumn;
			this.joinedTableColumn = joinedTableColumn;
			this.parentTableAlias = parentTableAlias;
			this.joinedTableAlias = joinedTableAlias;
		}

		public string Expression
		{
			get
			{
				return JoinedTemplate.FormatCurrentCulture(this.parentTableAlias, this.parentTableColumn, this.joinedTableAlias, this.joinedTableColumn);
			}
		}
	}
}
