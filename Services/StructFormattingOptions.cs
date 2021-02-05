using System;

namespace IO.Services
{
	public class StructFormattingOptions
	{
		public int IdentSize { get; set; } = 2;
		public string Elipsis { get; set; } = "...";
		public string MemberPrefix { get; set; } = "|";
		public string ComplexMemberPrefix { get; set; } = "¤";
		public int MaxArrayStringLength { get; set; } = 100;
		public string NewLine { get; set; } = Environment.NewLine;
	}
}
