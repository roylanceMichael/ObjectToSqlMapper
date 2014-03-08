namespace ObjectToSqlMapper.Utils
{
	using System.Globalization;

	public static class StringExtensions
	{
		public const string Tab = "\t";

		public static string FormatCurrentCulture(this string template, params object[] args)
		{
			return string.Format(CultureInfo.CurrentCulture, template, args);
		}
	}
}
