namespace ObjectToSqlMapper.Utils
{
	using System.Globalization;

	public static class StringExtensions
	{
		public const string Tab = "\t";

		public const string EntityFormatSqlServer = "[";

		public const string EntityFormatSqlServerEnd = "]";

		public const string EntityFormatOracle = "";

		public const string EntityFormatOracleEnd = "";

		public static FormatSystemModel BuildFormatSystemModel(this SqlType sqlType)
		{
			var isSqlServer = sqlType == SqlType.SqlServer;

			return new FormatSystemModel
				       {
								 Beginning = isSqlServer ? EntityFormatSqlServer : EntityFormatOracle,
								 Ending = isSqlServer ? EntityFormatSqlServerEnd : EntityFormatOracleEnd
				       };
		}

		public static string FormatCurrentCulture(this string template, params object[] args)
		{
			return string.Format(CultureInfo.CurrentCulture, template, args);
		}
	}
}
