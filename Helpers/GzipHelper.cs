using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.IO.Compression;
using System.Text;
using Common.Shared.Min.Extensions;

namespace IO.Helpers
{
	public static class GzipHelper
	{
		public static byte[] Compress([NotNull] string value, Encoding? encoding = null)
		{
			value.ThrowIfNull(nameof(value));

			encoding ??= Encoding.ASCII;

			return Compress(encoding.GetBytes(value));
		}

		public static byte[] Compress([NotNull] byte[] bytes)
		{
			bytes.ThrowIfNull(nameof(bytes));

			using var memoryStream = new MemoryStream();
			using var gZipStream = new GZipStream(memoryStream, CompressionMode.Compress, true);

			gZipStream.Write(bytes);

			return memoryStream.ToArray();
		}

		public static string Decompress([NotNull] Stream stream, Encoding? encoding)
		{
			stream.ThrowIfNull(nameof(stream));

			encoding ??= Encoding.ASCII;

			return encoding.GetString(Decompress(stream));
		}

		public static byte[] Decompress([NotNull] Stream stream)
		{
			stream.ThrowIfNull(nameof(stream));

			using var gZipStream = new GZipStream(stream, CompressionMode.Decompress, true);
			using var memoryStreamOutput = new MemoryStream();

			gZipStream.CopyTo(memoryStreamOutput);
			return memoryStreamOutput.ToArray();
		}
	}
}
