namespace ObjectToSqlMapper
{
	using ObjectToSqlMapper.Utils;

	public class PrimaryColumn : Column
	{
		public PrimaryColumn(string field, IAliasable table, SqlType sqlType = SqlType.SqlServer)
			: base(field, table, sqlType)
		{
		}
	}
}
