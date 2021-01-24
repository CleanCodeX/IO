using System.Diagnostics.CodeAnalysis;
using IO.Models;

namespace IO.Extensions
{
	public static class MultiSegmentFileExtensions
	{
		public static byte GetOffsetByte([NotNull] this IMultiSegmentFile source, int index, int offset) => source.GetOffsetByte(source.SegmentToAbsoluteOffset(index, offset));

		public static uint GetOffsetUInt16([NotNull] this IMultiSegmentFile source, int index, int offset) => source.GetOffsetUInt16(source.SegmentToAbsoluteOffset(index, offset));

		public static uint GetOffsetUInt32([NotNull] this IMultiSegmentFile source, int index, int offset) => source.GetOffsetUInt32(source.SegmentToAbsoluteOffset(index, offset));

		public static byte[] GetOffsetBytes([NotNull] this IMultiSegmentFile source, int index, int offset, int length) => source.GetOffsetBytes(source.SegmentToAbsoluteOffset(index, offset), length);

		public static void SetOffsetValue([NotNull] this IMultiSegmentFile source, int index, int offset, byte value) => source.SetOffsetValue(source.SegmentToAbsoluteOffset(index, offset), value);

		public static void SetOffsetValue([NotNull] this IMultiSegmentFile source, int index, int offset, ushort value) => source.SetOffsetValue(source.SegmentToAbsoluteOffset(index, offset), value);

		public static void SetOffsetValue([NotNull] this IMultiSegmentFile source, int index, int offset, uint value) => source.SetOffsetValue(source.SegmentToAbsoluteOffset(index, offset), value);

		public static void SetOffsetBytes([NotNull] this IMultiSegmentFile source, int index, int offset, byte[] bytes) => source.SetOffsetBytes(source.SegmentToAbsoluteOffset(index, offset), bytes);
	}
}