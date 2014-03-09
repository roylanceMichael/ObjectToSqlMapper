namespace ObjectToSqlMapper
{
	public class NormalColumn : Column
	{
		public NormalColumn(string field, IAliasable table)
			: base(field, table)
		{
		}
	}
}
