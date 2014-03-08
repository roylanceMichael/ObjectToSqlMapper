namespace ObjectToSqlMapper
{
	using ObjectToSqlMapper.Utils;

	public abstract class Column : ISqlConstituent
	{
		protected Column(string field)
		{
			this.Field = field;
		}

		public string Field { get; private set; }

		public string Expression
		{
			get
			{
				return StringExtensions.Tab + this.Field;
			}
		}
	}
}
