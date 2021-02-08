using System.IO;

namespace IO.Helpers
{
	/// <summary>
	/// Ensures existingness of directories
	/// </summary>
	public static class DirectoryHelper
	{
		/// <summary>
		/// Creates the file's directory if necessary.
		/// </summary>
		/// <param name="filePath">The file's directory to check</param>
		public static void EnsureFileDirectoryExists(in string filePath) => EnsureDirectoryExists(Path.GetDirectoryName(filePath)!);

		/// <summary>
		/// Creates the directory if necessary.
		/// </summary>
		/// <param name="directoryPath">The directory to check</param>
		public static void EnsureDirectoryExists(in string directoryPath)
		{
			if (!Directory.Exists(directoryPath))
				Directory.CreateDirectory(directoryPath!);
		}
	}
}
