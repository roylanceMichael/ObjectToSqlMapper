namespace ObjectToSqlMapper
{
	using ObjectToSqlMapper.Utils;

	public abstract class Column : ISqlConstituent
	{
		private const string AliasExpressionTemplate = StringExtensions.Tab + "{2}{0}{3}.{2}{1}{3}";

		private const string NoAliasExpressionTemplate = StringExtensions.Tab + "{1}{0}{2}";

		private readonly FormatSystemModel formatModel;

		protected Column(string field, SqlType sqlType)
		{
			this.Field = field;
			this.TableAlias = string.Empty;
			this.formatModel = sqlType.BuildFormatSystemModel();
		}

		protected Column(string field, IAliasable table, SqlType sqlType) : this(field, sqlType)
		{
			table.CheckWhetherArgumentIsNull("table");
			this.TableAlias = table.Alias;
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
					string.IsNullOrWhiteSpace(this.TableAlias) ?
							NoAliasExpressionTemplate.FormatCurrentCulture(this.Field, this.formatModel.Beginning, this.formatModel.Ending) :
							AliasExpressionTemplate.FormatCurrentCulture(this.TableAlias, this.Field, this.formatModel.Beginning, this.formatModel.Ending);
			}
		}

		protected string TableAlias { get; set; }
	}
}
