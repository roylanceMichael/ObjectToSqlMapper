namespace ObjectToSqlMapper
{
	using ObjectToSqlMapper.Utils;

	public abstract class Column : ISqlConstituent
	{
		private const string AliasExpressionTemplate = StringExtensions.Tab + "{0}.{1}";

		private const string NoAliasExpressionTemplate = StringExtensions.Tab + "{0}";

		private string tableAlias;

		protected Column(string field)
		{
			this.Field = field;
			this.tableAlias = string.Empty;
		}

		public string Field { get; private set; }

		public string Name
		{
			get
			{
				return this.Field;
			}
		}

		public virtual string Expression
		{
			get
			{
				return
					string.IsNullOrWhiteSpace(this.tableAlias) ?
							NoAliasExpressionTemplate.FormatCurrentCulture(this.Field) :
							AliasExpressionTemplate.FormatCurrentCulture(this.tableAlias, this.Field);
			}
		}

		public void SetTableAlias(string tblAlias)
		{
			this.tableAlias = tblAlias;
		}
	}
}
