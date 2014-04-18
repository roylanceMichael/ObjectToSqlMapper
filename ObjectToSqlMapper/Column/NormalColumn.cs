namespace ObjectToSqlMapper
{
	using ObjectToSqlMapper.Utils;

	public class NormalColumn : Column
	{
		public NormalColumn(string field, IAliasable table, SqlType sqlType = SqlType.SqlServer)
			: base(field, table, sqlType)
		{
		}
	}
}
