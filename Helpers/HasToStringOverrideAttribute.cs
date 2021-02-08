using System;

namespace IO.Helpers
{
	/// <summary>
	/// Indicates that this struct or class has a custom ToString override
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
	public class HasToStringOverrideAttribute : Attribute
	{
	}
}
