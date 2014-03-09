namespace ObjectToSqlMapper
{
	public class ForeignColumn : Column
	{
		public ForeignColumn(string field) : base(field)
		{			
		}

		public void SetTableAlias(string tblAlias)
		{
			this.TableAlias = tblAlias;
		}
	}
}
