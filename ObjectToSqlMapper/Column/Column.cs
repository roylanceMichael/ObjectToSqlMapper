namespace ObjectToSqlMapper
{
	using System.Dynamic;

	using ObjectToSqlMapper.Utils;

	public abstract class Column : ISqlConstituent
	{
		private const string AliasExpressionTemplate = StringExtensions.Tab + "{0}.{1}";

		private const string NoAliasExpressionTemplate = StringExtensions.Tab + "{0}";

		protected Column(string field)
		{
			this.Field = field;
			this.TableAlias = string.Empty;
		}

		protected Column(string field, IAliasable table) : this(field)
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
							NoAliasExpressionTemplate.FormatCurrentCulture(this.Field) :
							AliasExpressionTemplate.FormatCurrentCulture(this.TableAlias, this.Field);
			}
		}

		protected string TableAlias { get; set; }
	}
}
