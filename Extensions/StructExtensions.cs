using System.Diagnostics.CodeAnalysis;
using System.IO;
using Common.Shared.Min.Extensions;
using IO.Helpers;

namespace IO.Extensions
{
	public static class StructExtensions
	{
		/// <summary>
		/// Convert the bytes to a structure in host-endian format (little-endian on PCs).
		/// To use with big-endian data, reverse all of the data bytes and create a struct that is in the reverse order of the data.
		/// </summary>
		/// <typeparam name="T">The structure's type</typeparam>
		/// <param name="source">The buffer.</param>
		/// <returns></returns>
		public static T ToStruct<T>([NotNull] this byte[] source) where T : struct => StructSerializer.Deserialize<T>(source.GetOrThrowIfNull(nameof(source)));

		/// <summary>
		/// Converts the struct to a byte array in host-endian format (little-endian on PCs).
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source">The structure.</param>
		/// <returns></returns>
		public static byte[] ToBytes<T>([NotNull] this T source) 
			where T : struct => StructSerializer.Serialize(source);

		/// <summary>
		/// Converts the struct to a memory stream in host-endian format (little-endian on PCs).
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source">The structure.</param>
		/// <returns></returns>
		public static MemoryStream ToStream<T>([NotNull] this T source)
			where T : struct
		{
			source.ThrowIfNull(nameof(source));

			var ms = new MemoryStream();
			StructSerializer.Serialize(ms, source);
			return ms;
		}

		/// <summary>
		/// Formates the structure recursively by an optionally specified delimiter.
		/// </summary>
		/// <param name="source"></param>
		/// <param name="delimiter">The delimiter for separation</param>
		/// <returns></returns>
		public static string FormatAsString<T>(this T source, string? delimiter = null)
			where T : struct => StructFormatter.FormatAsString(source, 1, delimiter);
	}
}
