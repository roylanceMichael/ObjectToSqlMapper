namespace ObjectToSqlMapper.Table
{
	using ObjectToSqlMapper.Utils;

	public class Table : ISqlConstituent, IAliasable
	{
		private const string SqlTableTemplate = "FROM [{0}].[{1}] {2}";

		private const string NameTemplate = "[{0}].[{1}]";

		public Table(string schema, string table, string alias)
		{
			this.SchemaName = schema;
			this.TableName = table;
			this.Alias = alias;
		}

		public string SchemaName { get; private set; }

		public string TableName { get; private set; }

		public string Alias { get; private set; }

		public string Name
		{
			get
			{
				return NameTemplate.FormatCurrentCulture(this.SchemaName, this.TableName);
			}
		}

		public virtual string Expression
		{
			get
			{
				return SqlTableTemplate.FormatCurrentCulture(this.SchemaName, this.TableName, this.Alias);
			}
		}
	}
}
