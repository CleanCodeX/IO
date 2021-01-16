using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.InteropServices;
using Common.Shared.Min.Extensions;
using Common.Shared.Min.Helpers;

namespace SramCommons.Extensions
{
	public static class StreamExtensions
	{
		/// <summary>
		/// Copies the contents of input to output. Doesn't close either stream.
		/// </summary>
		public static MemoryStream Copy([NotNull] this Stream source)
		{
			source.ThrowIfNull(nameof(source));
			source.Position = 0;

			MemoryStream result = new();
			var buffer = new byte[source.Length];
			
			if (source.Read(buffer) > 0)
				result.Write(buffer);

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
	}
}
