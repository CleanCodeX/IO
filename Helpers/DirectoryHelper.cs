using System.IO;

namespace IO.Helpers
{
	/// <summary>
	/// Ensures existingness of directories
	/// </summary>
	public static class DirectoryHelper
	{
		/// <summary>
		/// Creates the file's directory if existing.
		/// </summary>
		/// <param name="filePath">The file's directory to check</param>
		public static void EnsureFileDirectoryExists(in string filePath) => EnsureDirectoryExists(Path.GetDirectoryName(filePath)!);

		/// <summary>
		/// Creates the directory if necessary.
		/// </summary>
		/// <param name="directoryPath">The directory to check</param>
		public static void EnsureDirectoryExists(in string directoryPath)
		{
			if (Directory.Exists(directoryPath)) return;

			Directory.CreateDirectory(directoryPath!);
		}

		/// <summary>
		/// Emties the directory if necessary, otherwise creates it
		/// </summary>
		/// <param name="directoryPath">The directory to check</param>
		public static void EnsureEmptyDirectoryExists(in string directoryPath)
		{
			EnsureDirectoryNotExists(directoryPath);
			EnsureDirectoryExists(directoryPath);
		}

		

		/// <summary>
		/// Deletes the directory if existing.
		/// </summary>
		/// <param name="directoryPath">The directory to check</param>
		/// <param name="recursive">Whether to delete the subdirectories recursively or not</param>
		public static void EnsureDirectoryNotExists(in string directoryPath, bool recursive = true)
		{
			if (!Directory.Exists(directoryPath)) return;
			
			Directory.Delete(directoryPath, recursive);
		}
	}
}
