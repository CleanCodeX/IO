using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace IO.Models.Structs
{
	/// <summary>
	/// 40 bit (5 byte) structure
	/// </summary>
	[StructLayout(LayoutKind.Sequential, Pack = 1, Size = 5)]
	[DebuggerDisplay("{ToString(),nq}")]
	public struct UInt40
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
		public byte[] Data;

		public ulong Value
		{
			get => BitConverter.ToUInt64(Data.AsSpan()[3..]);
			set => Data = BitConverter.GetBytes(value)[3..];
		}

		public override string ToString() => Value.ToString();
	}
}
