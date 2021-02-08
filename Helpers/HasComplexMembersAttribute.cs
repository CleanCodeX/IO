using System;

namespace IO.Helpers
{
	/// <summary>
	/// Marks the struct having complex struct members. Used to decide whether it's necessary to enumerate struct-field's fields or not.
	/// </summary>
	[AttributeUsage(AttributeTargets.Struct)]
	public class HasComplexMembersAttribute : Attribute
	{
	}
}
