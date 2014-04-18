namespace ObjectToSqlMapper.Table
{
	using ObjectToSqlMapper.Utils;

	public class Table : ISqlConstituent, IAliasable
	{
		private const string SqlTableTemplate = "FROM {3}{0}{4}.{3}{1}{4} {2}";

		private const string NameTemplate = "{2}{0}{3}.{2}{1}{3}";

		private readonly FormatSystemModel formatModel;

		public Table(string schema, string table, string alias, SqlType sqlType = SqlType.SqlServer)
		{
			this.SchemaName = schema;
			this.TableName = table;
			this.Alias = alias;
			this.formatModel = sqlType.BuildFormatSystemModel();
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
				return SqlTableTemplate.FormatCurrentCulture(this.SchemaName, this.TableName, this.Alias, this.formatModel.Beginning, this.formatModel.Ending);
			}
		}
	}
}
