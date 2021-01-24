using System.Diagnostics.CodeAnalysis;
using System.IO;
using Common.Shared.Min.Extensions;
using Common.Shared.Min.Helpers;
using IO.Models;

namespace IO.Extensions
{
	public static class RawSaveExtensions
	{
		public static void RawSave([NotNull] this IRawSave source, [NotNull] string filepath)
		{
			source.ThrowIfNull(nameof(source));
			Requires.NotNullOrEmpty(filepath, nameof(filepath));

			using var stream = new FileStream(filepath, FileMode.Create, FileAccess.Write);
			source.RawSave(stream);
		}
	}
}