using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Common.Shared.Min.Extensions;

namespace IO.Models.Structs
{
	/// <summary>
	/// 24 bit (3 byte) enum structure
	/// </summary>
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	[DebuggerDisplay("{ToString(),nq}")]
	public struct UInt24<TEnum>
		where TEnum : struct, Enum
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
		public byte[] Data;

		public TEnum Value
		{
			get => (TEnum)(object)BitConverter.ToUInt32(Data.Resize(4));
			set => Data = BitConverter.GetBytes((uint)(object)value)[1..];
		}

		public override string ToString() => Value.ToString();

		public static implicit operator UInt24<TEnum>(byte[] source) => new() { Data = source };
		public static implicit operator UInt24<TEnum>(TEnum source) => new() { Value = source };
		public static implicit operator byte[](UInt24<TEnum> source) => source.Data;
		public static implicit operator TEnum(UInt24<TEnum> source) => source.Value;
	}

	/// <summary>
	/// 24 bit (3 byte) structure
	/// </summary>
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	[DebuggerDisplay("{ToString(),nq}")]
	public struct UInt24
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
		public byte[] Data;

		public uint Value
		{
			get => BitConverter.ToUInt32(Data.Resize(4));
			set => Data = BitConverter.GetBytes(value)[1..];
		}

		public override string ToString() => Value.ToString();

		public static implicit operator UInt24(byte[] source) => new() { Data = source };
		public static implicit operator UInt24(uint source) => new() { Value = source };
		public static implicit operator byte[](UInt24 source) => source.Data;
		public static implicit operator uint(UInt24 source) => source.Value;
	}
}
