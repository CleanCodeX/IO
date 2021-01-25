using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace IO.Models.Structs
{
	/// <summary>
	/// 56 bit (7 byte) enum structure
	/// </summary>
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	[DebuggerDisplay("{ToString(),nq}")]
	public struct UInt56<TEnum>
		where TEnum : struct, Enum
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
		public byte[] Data;

		public TEnum Value
		{
			get => (TEnum)(object)BitConverter.ToUInt64(Data.AsSpan()[1..]);
			set => Data = BitConverter.GetBytes((ulong)(object)value)[1..];
		}

		public override string ToString() => Value.ToString();

		public static implicit operator UInt56<TEnum>(byte[] source) => new() { Data = source };
		public static implicit operator UInt56<TEnum>(TEnum source) => new() { Value = source };
		public static implicit operator byte[](UInt56<TEnum> source) => source.Data;
		public static implicit operator TEnum(UInt56<TEnum> source) => source.Value;
	}

	/// <summary>
	/// 56 bit (7 byte) structure
	/// </summary>
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
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

		public static implicit operator UInt56(byte[] source) => new() { Data = source };
		public static implicit operator UInt56(ulong source) => new() { Value = source };
		public static implicit operator byte[](UInt56 source) => source.Data;
		public static implicit operator ulong(UInt56 source) => source.Value;
	}
}
