namespace ObjectToSqlMapper.Table
{
	using ObjectToSqlMapper.Utils;

	public class Table : ISqlConstituent
	{
		private const string SqlTableTemplate = "FROM [{0}].[{1}] as {2}";

		public Table(string schema, string table, string alias)
		{
			this.SchemaName = schema;
			this.TableName = table;
			this.Alias = alias;
		}

		public string SchemaName { get; private set; }

		public string TableName { get; private set; }

		public string Alias { get; private set; }

		public string Expression
		{
			get
			{
				return SqlTableTemplate.FormatCurrentCulture(this.SchemaName, this.TableName, this.Alias);
			}
		}
	}
}
