namespace IO.Services
{
	public class ArrayFormattingOptions
	{
		public string Delimiter { get; set; } = ", ";
		public string FormatPrefix { get; set; } = "{";
		public string FormatPostfix { get; set; } = "}";
		public string NullValue { get; set; } = "Null";
	}
}
