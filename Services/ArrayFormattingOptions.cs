namespace IO.Services
{
	public class ArrayFormattingOptions
	{
		public string Delimiter { get; set; } = ", ";
		public string Prefix { get; set; } = "{";
		public string Suffix { get; set; } = "}";
		public string NullValue { get; set; } = "Null";
	}
}
