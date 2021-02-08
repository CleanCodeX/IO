using System;

namespace IO.Helpers
{

	/// <summary>
	/// Placed on a field will mark the field as grouping member. Placed on a struct will mark all fields as grouping members.
	/// </summary>
	[AttributeUsage(AttributeTargets.Struct | AttributeTargets.Field)]
	public class UseMemberGroupingAttribute : Attribute
	{
	}
}
