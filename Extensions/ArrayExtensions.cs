using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using Common.Shared.Min.Extensions;

namespace IO.Extensions
{
	public static class ArrayExtensions
	{
		public static byte[] Resize([NotNull] this byte[] source, int newSize)
		{
			Array.Resize(ref source, newSize);

			return source;
		}

		public static MemoryStream? ToStreamIfNotNull(this byte[]? source)
		{
			if (source is null) return null;

			return new(source);
		}

		/// <summary>Creates a memory stream</summary>
		/// <param name="source"></param>
		/// <returns>New memory stream</returns>
		public static MemoryStream ToStream([NotNull] this byte[] source) => new(source);

		/// <summary>
		/// Formates bytes with a delimiter.
		/// </summary>
		/// <param name="source"></param>
		/// <param name="delimiter">The delimiter for sepa</param>
		/// <returns></returns>
		public static string FormatAsString([NotNull] this Array source, string? delimiter = null)
		{
			source.ThrowIfNull(nameof(source));
			StringBuilder sb = new(source.Length);

			for (var i = 0; i < source.Length; i++)
			{
				if (i > 0)
					sb.Append(delimiter ?? ", ");

				sb = sb.Append(source.GetValue(i)?.ToString() ?? "null");
			}

			return $"{{{sb}}}";
		}

		/// <returns>Non 0-char trimmed string</returns>
		public static string GetString([NotNull] this byte[] source)
		{
			source.ThrowIfNull(nameof(source));

			var count = Array.IndexOf(source, (char)0, 0);
			if (count < 0) count = source.Length;

			var chars = new char[count / sizeof(char)];
			Buffer.BlockCopy(source, 0, chars, 0, count);
			return new string(chars);
		}

		/// <returns>0-char trimmed string</returns>
		public static string GetString([NotNull] this char[] source)
		{
			source.ThrowIfNull(nameof(source));

			var count = Array.IndexOf(source, (char)0, 0);
			if (count < 0) count = source.Length;

			var chars = new char[count];
			Buffer.BlockCopy(source, 0, chars, 0, count * sizeof(char));

			return new string(chars);
		}

		/// <returns>0-char trimmed ASCII string</returns>
		public static string GetStringAscii([NotNull] this byte[] source)
		{
			source.ThrowIfNull(nameof(source));
			var count = Array.IndexOf<byte>(source, 0, 0);
			if (count < 0) count = source.Length;
			return Encoding.ASCII.GetString(source, 0, count);
		}

		/// <returns>Array of chars</returns>
		public static char[] GetChars([NotNull] this byte[] source) => Encoding.ASCII.GetChars(source);
	}
}
