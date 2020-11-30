using System.IO;
using Common.Shared.Min.Helpers;
using SramCommons.Models;

namespace SramCommons.Extensions
{
	public static class SramFileExtensions
	{
		public static void Load(this ISramFile source, string filepath)
		{
			Requires.FileExists(filepath, nameof(filepath));

			source.Load(new FileStream(filepath, FileMode.Open, FileAccess.Read));
		}

		public static void Save(this ISramFile source, string filepath)
		{
			Requires.NotNullOrEmpty(filepath, nameof(filepath));

			source.Save(new FileStream(filepath, FileMode.Create, FileAccess.Write));
		}

		public static void RawSave(this IRawSave source, string filepath)
		{
			Requires.NotNullOrEmpty(filepath, nameof(filepath));

			source.RawSave(new FileStream(filepath, FileMode.Create, FileAccess.Write));
		}
	}
}