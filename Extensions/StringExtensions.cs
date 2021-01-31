namespace IO.Extensions
{
	public static class StringExtensions
	{
		public static string? ToNullIfEmpty(this string? source) => source == string.Empty ? null : source;
	}
}
