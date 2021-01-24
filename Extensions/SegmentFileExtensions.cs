using System.Diagnostics.CodeAnalysis;
using IO.Models;

namespace IO.Extensions
{
	public static class SegmentFileExtensions
	{
		public static byte GetOffsetByte([NotNull] this ISegmentFile source, int offset) => source.GetOffsetByte(source.SegmentToAbsoluteOffset(offset));

		public static uint GetOffsetUInt16([NotNull] this ISegmentFile source, int offset) => source.GetOffsetUInt16(source.SegmentToAbsoluteOffset(offset));

		public static uint GetOffsetUInt32([NotNull] this ISegmentFile source, int offset) => source.GetOffsetUInt32(source.SegmentToAbsoluteOffset(offset));

		public static byte[] GetOffsetBytes([NotNull] this ISegmentFile source, int offset, int length) => source.GetOffsetBytes(source.SegmentToAbsoluteOffset(offset), length);

		public static void SetOffsetValue([NotNull] this ISegmentFile source, int offset, byte value) => source.SetOffsetValue(source.SegmentToAbsoluteOffset(offset), value);

		public static void SetOffsetValue([NotNull] this ISegmentFile source, int offset, ushort value) => source.SetOffsetValue(source.SegmentToAbsoluteOffset(offset), value);

		public static void SetOffsetValue([NotNull] this ISegmentFile source, int offset, uint value) => source.SetOffsetValue(source.SegmentToAbsoluteOffset(offset), value);

		public static void SetOffsetBytes([NotNull] this ISegmentFile source, int offset, byte[] bytes) => source.SetOffsetBytes(source.SegmentToAbsoluteOffset(offset), bytes);
	}
}