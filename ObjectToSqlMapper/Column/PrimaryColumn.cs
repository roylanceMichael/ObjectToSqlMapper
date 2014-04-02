namespace ObjectToSqlMapper
{
	public class PrimaryColumn : Column
	{
		public PrimaryColumn(string field, IAliasable table)
			: base(field, table)
		{
		}
	}
}
