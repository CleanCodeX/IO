using System.IO;

namespace IO.Helpers
{
	public static class FileHelper
	{
		/// <summary>
		/// Deletes the file if existing.
		/// </summary>
		/// <param name="filePath">The file path to check</param>
		public static void EnsureFileNotExists(in string filePath)
		{
			if (!File.Exists(filePath)) return;
			
			File.Delete(filePath);
		}
	}
}