using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace IO.Models.Structs
{
	/// <summary>
	/// 24 bit (3 byte) structure
	/// </summary>
	[StructLayout(LayoutKind.Sequential, Pack = 1, Size = 3)]
	[DebuggerDisplay("{ToString(),nq}")]
	public struct UInt24
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
		public byte[] Data;

		public uint Value
		{
			get => BitConverter.ToUInt32(Data.AsSpan()[1..]);
			set => Data = BitConverter.GetBytes(value)[1..];
		}

		public override string ToString() => Value.ToString();
	}
}
