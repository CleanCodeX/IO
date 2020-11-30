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

		public static uint GetOffsetValue(this ISramFile source, int offset, int length = 1)
		{
			source.ThrowIfNull(nameof(source));
			Requires.LessThan(offset, source.SramBuffer.Length, nameof(offset));

			var bytes = source.SramBuffer[offset..(offset+length+1)];

			return BitConverter.ToUInt32(bytes);
		}

		public static void SetOffsetValue(this ISramFile source, int offset, byte value)
		{
			source.ThrowIfNull(nameof(source));
			Requires.LessThan(offset, source.SramBuffer.Length, nameof(offset));

			source.SramBuffer[offset] = value;
		}

		public static void SetOffsetValue(this ISramFile source, int offset, ushort value)
		{
			source.ThrowIfNull(nameof(source));
			Requires.LessThan(offset, source.SramBuffer.Length, nameof(offset));

			var bytes = BitConverter.GetBytes(value);

			Array.Copy(source.SramBuffer, offset, bytes, 0, bytes.Length);
		}

		public static void SetOffsetValue(this ISramFile source, int offset, uint value)
		{
			source.ThrowIfNull(nameof(source));
			Requires.LessThan(offset, source.SramBuffer.Length, nameof(offset));

			var bytes = BitConverter.GetBytes(value);

			Array.Copy(source.SramBuffer, offset, bytes, 0, bytes.Length);
		}

		public static void RawSave(this IRawSave source, string filepath)
		{
			source.ThrowIfNull(nameof(source));
			Requires.NotNullOrEmpty(filepath, nameof(filepath));

			source.RawSave(new FileStream(filepath, FileMode.Create, FileAccess.Write));
		}
	}
}