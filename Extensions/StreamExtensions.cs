using System;
using System.IO;
using System.Runtime.InteropServices;

namespace SramCommons.Extensions
{
	public static partial class StreamExtensions
	{
		public static T Read<T>(this Stream source) where T: struct
		{
			T result = default;
			var size = Marshal.SizeOf(result);
			var data = new byte[size];

			source.Read(data.AsSpan());

			return data.ToStruct<T>();
		}

		public static T Read<T>(this Stream source, int offset) where T : struct
		{
			T result = default;
			var size = Marshal.SizeOf(result);
			var data = new byte[size];

			source.Read(data, offset, size);

			return data.ToStruct<T>();
		}
	}
}
