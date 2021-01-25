using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace IO.Models.Structs
{
	/// <summary>
	/// 48 bit (6 byte) structure
	/// </summary>
	[StructLayout(LayoutKind.Sequential, Pack = 1, Size = 6)]
	[DebuggerDisplay("{ToString(),nq}")]
	public struct UInt48
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
		public byte[] Data;

		public ulong Value
		{
			get => BitConverter.ToUInt64(Data.AsSpan()[2..]);
			set => Data = BitConverter.GetBytes(value)[2..];
		}

		public override string ToString() => Value.ToString();
	}
}
