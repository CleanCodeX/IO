using System;
using System.IO;
using Common.Shared.Min.Extensions;
using Common.Shared.Min.Helpers;
using SramCommons.Models;

namespace SramCommons.Extensions
{
	public static class SramFileExtensions
	{
		public static void Load(this ISramFile source, string filepath)
		{
			source.ThrowIfNull(nameof(source));
			Requires.FileExists(filepath, nameof(filepath));

			source.Load(new FileStream(filepath, FileMode.Open, FileAccess.Read));
		}

		public static void Save(this ISramFile source, string filepath)
		{
			source.ThrowIfNull(nameof(source));
			Requires.NotNullOrEmpty(filepath, nameof(filepath));

			source.Save(new FileStream(filepath, FileMode.Create, FileAccess.Write));
		}

		public static byte GetOffsetByte(this ISramFile source, int gameIndex, int offset) => source.GetOffsetByte(source.GameToSramOffset(gameIndex, offset));
		public static byte GetOffsetByte(this ISramFile source, int offset)
		{
			source.ThrowIfNull(nameof(source));
			return source.GetOffsetBytes(offset, 1)[0];
		}

		public static uint GetOffsetUInt16(this ISramFile source, int gameIndex, int offset) => source.GetOffsetUInt16(source.GameToSramOffset(gameIndex, offset));
		public static uint GetOffsetUInt16(this ISramFile source, int offset)
		{
			source.ThrowIfNull(nameof(source));
			return BitConverter.ToUInt16(source.GetOffsetBytes(offset, 2));
		}

		public static uint GetOffsetUInt32(this ISramFile source, int gameIndex, int offset) => source.GetOffsetUInt32(source.GameToSramOffset(gameIndex, offset));
		public static uint GetOffsetUInt32(this ISramFile source, int offset)
		{
			source.ThrowIfNull(nameof(source));
			return BitConverter.ToUInt32(source.GetOffsetBytes(offset, 4));
		}

		public static byte[] GetOffsetBytes(this ISramFile source, int gameIndex, int offset, int length) => source.GetOffsetBytes(source.GameToSramOffset(gameIndex, offset), length);
		public static byte[] GetOffsetBytes(this ISramFile source, int offset, int length = 1)
		{
			source.ThrowIfNull(nameof(source));
			Requires.LessThanOrEqual(offset + length, source.SramBuffer.Length, nameof(offset));

			return source.SramBuffer[offset..(offset + length)];
		}

		public static void SetOffsetValue(this ISramFile source, int gameIndex, int offset, byte value) => source.SetOffsetValue(source.GameToSramOffset(gameIndex, offset), value);

		public static void SetOffsetValue(this ISramFile source, int offset, byte value)
		{
			source.ThrowIfNull(nameof(source));
			source.SetOffsetBytes(offset, new [] { value });
		}

		public static void SetOffsetValue(this ISramFile source, int gameIndex, int offset, ushort value) => source.SetOffsetValue(source.GameToSramOffset(gameIndex, offset), value);

		public static void SetOffsetValue(this ISramFile source, int offset, ushort value)
		{
			source.ThrowIfNull(nameof(source));
			source.SetOffsetBytes(offset, BitConverter.GetBytes(value));
		}

		public static void SetOffsetValue(this ISramFile source, int gameIndex, int offset, uint value) => source.SetOffsetValue(source.GameToSramOffset(gameIndex, offset), value);

		public static void SetOffsetValue(this ISramFile source, int offset, uint value)
		{
			source.ThrowIfNull(nameof(source));
			source.SetOffsetBytes(offset, BitConverter.GetBytes(value));
		}

		public static void SetOffsetBytes(this ISramFile source, int gameIndex, int offset, byte[] bytes) => source.SetOffsetBytes(source.GameToSramOffset(gameIndex, offset), bytes);
		public static void SetOffsetBytes(this ISramFile source, int offset, byte[] bytes)
		{
			source.ThrowIfNull(nameof(source));
			Requires.LessThan(offset, source.SramBuffer.Length, nameof(offset));

			Array.Copy(bytes, 0, source.SramBuffer, offset, bytes.Length);
		}

		public static void RawSave(this IRawSave source, string filepath)
		{
			source.ThrowIfNull(nameof(source));
			Requires.NotNullOrEmpty(filepath, nameof(filepath));

			source.RawSave(new FileStream(filepath, FileMode.Create, FileAccess.Write));
		}
	}
}