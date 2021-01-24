using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Common.Shared.Min.Extensions;
using Common.Shared.Min.Helpers;
using IO.Models;

namespace IO.Extensions
{
	public static class BlobFileFileExtensions
	{
		public static void Load([NotNull] this IBlobFile source, [NotNull] string filepath)
		{
			source.ThrowIfNull(nameof(source));
			Requires.FileExists(filepath, nameof(filepath));

			using var stream = new FileStream(filepath, FileMode.Open, FileAccess.Read);
			source.Load(stream);
		}

		public static void Save([NotNull] this IBlobFile source, [NotNull] string filepath)
		{
			source.ThrowIfNull(nameof(source));
			Requires.NotNullOrEmpty(filepath, nameof(filepath));

			using var stream = new FileStream(filepath, FileMode.Create, FileAccess.Write);
			source.Save(stream);
		}

		public static byte GetOffsetByte([NotNull] this IBlobFile source, int offset)
		{
			source.ThrowIfNull(nameof(source));
			return source.GetOffsetBytes(offset, 1)[0];
		}

		public static byte[] GetOffsetBytes([NotNull] this IBlobFile source, int offset, int length = 1)
		{
			source.ThrowIfNull(nameof(source));
			Requires.LessThanOrEqual(offset + length, source.Buffer.Length, nameof(offset));

			return source.Buffer[offset..(offset + length)];
		}

		public static void SetOffsetBytes([NotNull] this IBlobFile source, int offset, [NotNull] byte[] bytes)
		{
			source.ThrowIfNull(nameof(source));
			bytes.ThrowIfNull(nameof(bytes));
			Requires.LessThan(offset, source.Buffer.Length, nameof(offset));

			Array.Copy(bytes, 0, source.Buffer, offset, bytes.Length);
		}

		public static uint GetOffsetUInt16([NotNull] this IBlobFile source, int offset)
		{
			source.ThrowIfNull(nameof(source));
			return BitConverter.ToUInt16(source.GetOffsetBytes(offset, 2));
		}

		public static uint GetOffsetUInt32([NotNull] this IBlobFile source, int offset)
		{
			source.ThrowIfNull(nameof(source));
			return BitConverter.ToUInt32(source.GetOffsetBytes(offset, 4));
		}

		public static void SetOffsetValue([NotNull] this IBlobFile source, int offset, byte value)
		{
			source.ThrowIfNull(nameof(source));
			source.SetOffsetBytes(offset, new[] {value});
		}

		public static void SetOffsetValue([NotNull] this IBlobFile source, int offset, ushort value)
		{
			source.ThrowIfNull(nameof(source));
			source.SetOffsetBytes(offset, BitConverter.GetBytes(value));
		}

		public static void SetOffsetValue([NotNull] this IBlobFile source, int offset, uint value)
		{
			source.ThrowIfNull(nameof(source));
			source.SetOffsetBytes(offset, BitConverter.GetBytes(value));
		}
	}
}