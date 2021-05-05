using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.InteropServices;
using Common.Shared.Min.Extensions;
using IO.Helpers;

namespace IO.Extensions
{
	public static class StreamExtensions
	{
		public static void SetSlice([NotNull] this Stream source, byte[] buffer) => source.SetSlice(0, buffer);
		public static void SetSlice([NotNull] this Stream source, long streamPosition, byte[] buffer)
		{
			source.Position = streamPosition;
			source.Write(buffer, 0, buffer.Length);
		}

		public static byte[] GetBytes([NotNull] this Stream source)
		{
			if (source is MemoryStream ms && ms.TryGetBuffer(out var msBuffer))
				return msBuffer.Array!;

			var buffer = new byte[source.Length];

			source.Position = 0;
			source.Read(buffer, 0, buffer.Length);

			return buffer;
		}

		public static MemoryStream GetSlice([NotNull] this Stream source, int length) => source.GetSlice(0, length);
		public static MemoryStream GetSlice([NotNull] this Stream source, long streamPosition, int length)
		{
			var buffer = new byte[length];

			source.Position = streamPosition;
			source.Read(buffer, 0, length);

			return new(buffer);
		}

		/// <summary>
		/// Copies the contents of input to output. Doesn't close either stream.
		/// </summary>
		public static MemoryStream Copy([NotNull] this Stream source)
		{
			source.ThrowIfNull(nameof(source));
			source.Position = 0;

			MemoryStream result = new();

			source.CopyTo(result);
			result.Position = 0;

			return result;
		}

		public static T Read<T>([NotNull] this Stream source) where T: struct
		{
			source.ThrowIfNull(nameof(source));

			T result = default;
			var size = Marshal.SizeOf(result);
			var data = new byte[size];

			source.Read(data.AsSpan());

			return data.ToStruct<T>();
		}

		public static T Read<T>([NotNull] this Stream source, int offset) where T : struct
		{
			source.ThrowIfNull(nameof(source));

			T result = default;
			var size = Marshal.SizeOf(result);
			var data = new byte[size];

			source.Read(data, offset, size);

			return data.ToStruct<T>();
		}

		/// <summary>
		/// Appends the contents of the stream to file
		/// </summary>
		public static void AppendTo([NotNull] this Stream source, [NotNull] string filePath)
		{
			DirectoryHelper.EnsureFileDirectoryExists(filePath);

			using var fileStream = new FileStream(filePath, FileMode.Append, FileAccess.Write, FileShare.Read);
			source.CopyAllTo(fileStream);
		}

		/// <summary>
		/// Appends the whole contents of the stream to a file
		/// </summary>
		public static void SaveTo([NotNull] this Stream source, [NotNull] string filePath)
		{
			DirectoryHelper.EnsureFileDirectoryExists(filePath);

			using var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.Read);
			source.CopyAllTo(source);
		}
	}
}
