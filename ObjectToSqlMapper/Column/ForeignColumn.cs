namespace ObjectToSqlMapper
{
	using ObjectToSqlMapper.Utils;

	public class ForeignColumn : Column
	{
		public ForeignColumn(string field, SqlType sqlType = SqlType.SqlServer) : base(field, sqlType)
		{			
		}

		public void SetTableAlias(string tblAlias)
		{
			this.TableAlias = tblAlias;
		}
	}
}
