using System;

namespace IO.Services
{
	[Flags]
	public enum ShowTypeNamesOption
	{
		None = 0,
		GroupingMembers = 0x1,
		NonGroupingMembers = 0x2,
		All = GroupingMembers | NonGroupingMembers,
	}

	public class StructFormattingOptions
	{
		public int IdentSize { get; set; } = 2;
		public string MemberDelimiter { get; set; } = Environment.NewLine;
		public string SimpleMemberPrefix { get; set; } = "|";
		public string ComplexMemberPrefix { get; set; } = "¤";
		public string Ellipsis { get; set; } = "...";
		public int ArrayStringMaxLength { get; set; } = 100;
		public ShowTypeNamesOption ShowTypeNames { get; set; } = ShowTypeNamesOption.GroupingMembers;
		public string? TypeNameTemplate { get; set; } = "[{0}] {1}";
	}
}
