using ObjectExtensions.Copy;

namespace IO.Extensions
{
	/// <summary>
	/// Creates a copy of source
	/// </summary>
	public static class GenericExtensions
	{
		public static T Copy<T>(this T source) => CopyExtensions.Copy(source);
	}
}
