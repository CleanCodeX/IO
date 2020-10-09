using System;
using Common.Shared.Min.Extensions;

namespace SramCommons.Extensions
{
	public static class StringExtensions
	{
		private static readonly int _charSize = sizeof(char);

		public static unsafe byte[] GetBytes(string str)
		{
			str.ThrowIfNull(nameof(str));
			if (str.Length == 0) return Array.Empty<byte>();

			fixed (char* p = str)
				return new Span<byte>(p, str.Length * _charSize).ToArray();
		}

		public static unsafe string GetString(byte[] bytes)
		{
			bytes.ThrowIfNull(nameof(bytes));
			if (bytes.Length % _charSize != 0) throw new ArgumentException($"Invalid {nameof(bytes)} length");
			if (bytes.Length == 0) return string.Empty;

			fixed (byte* p = bytes)
				return new string(new Span<char>(p, bytes.Length / _charSize));
		}
	}
}
