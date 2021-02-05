using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using Common.Shared.Min.Extensions;
using IO.Services;

namespace IO.Extensions
{
	public static class ArrayExtensions
	{
		/// <summary>
		/// Formates the array.
		/// </summary>
		/// <param name="source"></param>
		/// <param name="options">The options to be used.</param>
		/// <returns>A formatted string</returns>
		public static string Format([NotNull] this Array source, ArrayFormattingOptions? options = default) => IOServices.ArrayFormatter.Format(source, options);

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
