using System;

namespace IO.Services
{
	[Flags]
	public enum ShowTypeNamesOption
	{
		None = 0,
		GroupingMembers = 0x1,
		AllMembers = 0x2 | GroupingMembers,
	}

	public class StructFormattingOptions
	{
		public int IdentSize = 2;
		public string MemberDelimiter = Environment.NewLine;
		public string SimpleMemberPrefix = "|";
		public string ComplexMemberPrefix = "¤";
		public string ArrayElementPrefix = "¬";

		/// <summary>
		/// The enum's typename will be inserted to a param {0} marker.
		/// </summary>
		public string EnumTypeSuffix = " (Enum)";
		public string GroupingMemberTemplate = "»{0}«";
		public string Ellipsis = "...";
		public int ArrayStringMaxLength = 100;
		public ShowTypeNamesOption ShowTypeNames = ShowTypeNamesOption.GroupingMembers;
		public string? TypeNameTemplate = "[{0}] {1}";
	}
}
