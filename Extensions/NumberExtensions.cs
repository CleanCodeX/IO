using System;
using System.Globalization;
using System.Linq;
using System.Text;

namespace IO.Extensions
{
	public static class NumberExtensions
	{
		public static ushort ReverseBytes(this ushort source) => BitConverter.ToUInt16(BitConverter.GetBytes(source).Reverse().ToArray());
		public static short ReverseBytes(this short source) => BitConverter.ToInt16(BitConverter.GetBytes(source).Reverse().ToArray());

		public static uint ReverseBytes(this uint source) => BitConverter.ToUInt32(BitConverter.GetBytes(source).Reverse().ToArray());
		public static int ReverseBytes(this int source) => BitConverter.ToInt32(BitConverter.GetBytes(source).Reverse().ToArray());

		public static string PadLeft(this ushort source) => source.ToString().PadLeft(5);
		public static string PadLeft(this short source) => source.ToString().PadLeft(6);
		public static string PadLeft(this uint source) => source.ToString().PadLeft(10);
		public static string PadLeft(this int source) => source.ToString().PadLeft(11);

		public static string FormatBinary(this byte value, int bitLength = 8) => InternalFormatBinary(value, bitLength);
		public static string FormatBinary(this short value, int bitLength = 16) => InternalFormatBinary(value, bitLength);
		public static string FormatBinary(this ushort value, int bitLength = 16) => InternalFormatBinary(value, bitLength);
		public static string FormatBinary(this int value, int bitLength = 32) => InternalFormatBinary(value, bitLength);
		public static string FormatBinary(this uint value, int bitLength = 32) => InternalFormatBinary(value, bitLength);
		public static string FormatBinary(this long value, int bitLength = 64) => InternalFormatBinary(value, bitLength);
		public static string FormatBinary(this ulong value, int bitLength = 64) => InternalFormatBinary((long)value, bitLength);

		private static string InternalFormatBinary(long value, int minBitLength)
		{
			if (minBitLength < 8)
				minBitLength = 8;

			if (minBitLength % 8 > 0)
				minBitLength = minBitLength / 8 * 9;

			var result = Convert.ToString(value, 2).PadLeft(minBitLength, '0');

			StringBuilder sb = new();
			for (var i = 0; i < result.Length; i += 8)
			{
				var byteBitString = SplitResult(i);

				if (i > 0)
					sb.Append(", ");

				sb.Append($"{byteBitString}");
			}

			return sb.ToString().TrimEnd();

			string SplitResult(int i) => result.Substring(i, 4) + "-" + result.Substring(i + 4, 4);
		}
	}
}
