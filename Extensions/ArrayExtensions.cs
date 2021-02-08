using System;
using System.Diagnostics.CodeAnalysis;
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
		public static string Format([NotNull, DisallowNull] this Array source, ArrayFormattingOptions? options = default) => IOServices.ArrayFormatter?.Format(source, options) ?? source.ToString()!;

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
		public static string GetAsciiString([NotNull] this byte[] source)
		{
			source.ThrowIfNull(nameof(source));
			var count = Array.IndexOf<byte>(source, 0, 0);
			if (count < 0) count = source.Length;
			return Encoding.ASCII.GetString(source, 0, count);
		}

		/// <returns>Array of chars</returns>
		public static char[] GetAsciiChars([NotNull] this byte[] source) => Encoding.ASCII.GetChars(source);
	}
}
