using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace IO.Models.Structs
{
	[StructLayout(LayoutKind.Sequential, Pack = 1, Size = 3)]
	[DebuggerDisplay("{ToString(),nq}")]
	public struct ThreeByteUInt
	{
		/// <summary>
		/// Gets or set 3 bytes values as uint
		/// </summary>
		public uint Value
		{
			get { return BitConverter.ToUInt32(new[] {Byte1, Byte2, Byte3, byte.MinValue}); }
			set
			{
				var bytes = BitConverter.GetBytes(value);
				Byte1 = bytes[0];
				Byte2 = bytes[1];
				Byte3 = bytes[2];
			}
		}

		public byte Byte1;
		public byte Byte2;
		public byte Byte3;

		public override string ToString() => Value.ToString();
	}
}
