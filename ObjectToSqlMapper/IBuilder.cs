namespace ObjectToSqlMapper
{
	public interface IBuilder<out T>
	{
		T Build();
	}
}
