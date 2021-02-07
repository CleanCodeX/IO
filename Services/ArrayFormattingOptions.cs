namespace IO.Services
{
	public class ArrayFormattingOptions
	{
		public string Delimiter { get; set; } = ", ";
		public string Prefix { get; set; } = "{";
		public string Postfix { get; set; } = "}";
		public string NullValue { get; set; } = "Null";
	}
}
