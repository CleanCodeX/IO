using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace IO.Models.Structs
{
	/// <summary>
	/// 40 bit (5 byte) structure
	/// </summary>
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	[DebuggerDisplay("{ToString(),nq}")]
	public struct UInt40<TEnum>
		where TEnum: struct, Enum
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
		public byte[] Data;

		public TEnum Value
		{
			get => (TEnum)(object)BitConverter.ToUInt64(Data.AsSpan()[3..]);
			set => Data = BitConverter.GetBytes((ulong)(object)value)[3..];
		}

		public override string ToString() => Value.ToString();

		public static implicit operator UInt40<TEnum>(byte[] source) => new() { Data = source };
		public static implicit operator UInt40<TEnum>(TEnum source) => new() { Value = source };
		public static implicit operator byte[](UInt40<TEnum> source) => source.Data;
		public static implicit operator TEnum(UInt40<TEnum> source) => source.Value;
	}

	/// <summary>
	/// 40 bit (5 byte) structure
	/// </summary>
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
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

		public static implicit operator UInt40(byte[] source) => new() { Data = source };
		public static implicit operator UInt40(ulong source) => new() { Value = source };
		public static implicit operator byte[](UInt40 source) => source.Data;
		public static implicit operator ulong(UInt40 source) => source.Value;
	}
}
