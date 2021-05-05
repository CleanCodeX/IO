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

		public static byte[] Compress([NotNull] byte[] buffer)
		{
			buffer.ThrowIfNull(nameof(buffer));

			using MemoryStream memoryStream = new();
			using GZipStream gZipStream = new(memoryStream, CompressionMode.Compress);
			
			gZipStream.Write(buffer);
			
			return memoryStream.GetBuffer();
		}

		public static string Decompress([NotNull] Stream stream, Encoding? encoding, bool leaveOpen = false)
		{
			stream.ThrowIfNull(nameof(stream));

			encoding ??= Encoding.ASCII;

			return encoding.GetString(Decompress(stream, leaveOpen));
		}

		public static byte[] Decompress([NotNull] byte[] buffer) => Decompress(new MemoryStream(buffer));
		public static byte[] Decompress([NotNull] Stream stream, bool leaveOpen = false)
		{
			stream.ThrowIfNull(nameof(stream));
			stream.Position = 0;

			using GZipStream gZipStream = new(stream, CompressionMode.Decompress, leaveOpen);
			using MemoryStream outputStream = new();
			gZipStream.CopyTo(outputStream);

			return outputStream.GetBuffer();
		}
	}
}
