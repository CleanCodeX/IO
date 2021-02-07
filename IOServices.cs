using IO.Services;

namespace IO
{
	public static class IOServices
	{
		public static IStructFormatter? StructFormatter { get; set; }
		public static IArrayFormatter? ArrayFormatter { get; set; }
	}
}