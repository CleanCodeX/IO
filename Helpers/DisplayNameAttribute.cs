using System;

namespace IO.Helpers
{
	[AttributeUsage(AttributeTargets.All)]
	public class DisplayNameAttribute : System.ComponentModel.DisplayNameAttribute
	{
		public DisplayNameAttribute(string displayName) : base(displayName)
		{ }
	}
}
