using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;

namespace SramCommons.Extensions
{
	public static class ArrayExtensions
	{
		public static string AsString([NotNull] this char[] source) => new(source);

		public static MemoryStream? ToStreamIfNotNull(this byte[]? source)
		{
			if (source is null) return null;

			return new(source);
		}

		public static MemoryStream ToStream([NotNull] this byte[] source) => new(source);

		public static string FormatAsString([NotNull] this byte[] source, string? delimiter = null)
		{
			StringBuilder sb = new(source.Length);

			for (var i = 0; i < source.Length; i++)
			{
				if (i > 0)
					sb.Append(delimiter ?? ", ");

				sb = sb.Append(source[i]);
			}

			return sb.ToString();
		}

		public static string GetString([NotNull] this byte[] buffer)
		{
			var chars = new char[buffer.Length / sizeof(char)];
			Buffer.BlockCopy(buffer, 0, chars, 0, buffer.Length);
			return new string(chars);
		}

		public static string GetString([NotNull] this char[] buffer)
		{
			var chars = new char[buffer.Length];
			Buffer.BlockCopy(buffer, 0, chars, 0, buffer.Length);
			return new string(chars);
		}

		public static string GetStringAscii([NotNull] this byte[] buffer)
		{
			var count = Array.IndexOf<byte>(buffer, 0, 0);
			if (count < 0) count = buffer.Length;
			return Encoding.ASCII.GetString(buffer, 0, count);
		}
	}
}
