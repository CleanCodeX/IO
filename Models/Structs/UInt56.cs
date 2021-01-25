using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace IO.Models.Structs
{
	/// <summary>
	/// 56 bit (7 byte) structure
	/// </summary>
	[StructLayout(LayoutKind.Sequential, Pack = 1, Size = 7)]
	[DebuggerDisplay("{ToString(),nq}")]
	public struct UInt56
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
		public byte[] Data;

		public ulong Value
		{
			get => BitConverter.ToUInt64(Data.AsSpan()[1..]);
			set => Data = BitConverter.GetBytes(value)[1..];
		}

		public override string ToString() => Value.ToString();
	}
}
