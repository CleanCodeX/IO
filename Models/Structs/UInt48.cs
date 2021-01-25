using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace IO.Models.Structs
{
	/// <summary>
	/// 48 bit (6 byte) enum structure
	/// </summary>
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	[DebuggerDisplay("{ToString(),nq}")]
	public struct UInt48<TEnum>
		where TEnum : struct, Enum
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
		public byte[] Data;

		public TEnum Value
		{
			get => (TEnum)(object)BitConverter.ToUInt64(Data.AsSpan()[2..]);
			set => Data = BitConverter.GetBytes((ulong)(object)value)[2..];
		}

		public override string ToString() => Value.ToString();

		public static implicit operator UInt48<TEnum>(byte[] source) => new() { Data = source };
		public static implicit operator UInt48<TEnum>(TEnum source) => new() { Value = source };
		public static implicit operator byte[](UInt48<TEnum> source) => source.Data;
		public static implicit operator TEnum(UInt48<TEnum> source) => source.Value;
	}

	/// <summary>
	/// 48 bit (6 byte) structure
	/// </summary>
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
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

		public static implicit operator UInt48(byte[] source) => new() { Data = source };
		public static implicit operator UInt48(ulong source) => new() { Value = source };
		public static implicit operator byte[](UInt48 source) => source.Data;
		public static implicit operator ulong(UInt48 source) => source.Value;
	}
}
