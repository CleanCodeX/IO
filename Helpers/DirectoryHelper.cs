using System.IO;

namespace IO.Helpers
{
	public static class DirectoryHelper
	{
		public static void EnsureFileDirectoryExists(in string filePath) => EnsureDirectoryExists(Path.GetDirectoryName(filePath)!);
		public static void EnsureDirectoryExists(in string directory)
		{
			if (!Directory.Exists(directory))
				Directory.CreateDirectory(directory!);
		}
	}
}
