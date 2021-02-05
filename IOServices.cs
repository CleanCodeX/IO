using IO.Services;

namespace IO
{
	public static class IOServices
	{
		private static IStructFormatter? defaultStructFormatter;

		public static IStructFormatter StructFormatter
		{
			get => defaultStructFormatter ??= new ConsoleStructFormatter();
			set => defaultStructFormatter = value;
		}

		private static IArrayFormatter? defaultArrayFormatter;

		public static IArrayFormatter ArrayFormatter
		{
			get => defaultArrayFormatter ??= new ConsoleArrayFormatter();
			set => defaultArrayFormatter = value;
		}
	}
}