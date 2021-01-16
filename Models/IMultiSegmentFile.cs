namespace SramCommons.Models
{
	/// <summary>Generic interface for multi segment file</summary>
	/// <typeparam name="TSegment">The segment's structure type</typeparam>
	public interface IMultiSegmentFile<TSegment> : IMultiSegmentFile
		where TSegment : struct
	{
		/// <summary>Gets the segment at specified index</summary>
		/// <param name="index">The index of the segment</param>
		TSegment GetSegment(int index);

		/// <summary>Sets a segment at specified index</summary>
		/// <param name="index">The index of the segment</param>
		/// <param name="segment">The segment to copy from</param>
		void SetSegment(int index, TSegment segment);
	}

	/// <summary>Non generic interface for a multi segment file</summary>
	public interface IMultiSegmentFile : IBlobFile
	{
		/// <summary>Offset of the first segment</summary>
		int FirstSegmentOffset { get; }
		/// <inheritdoc cref="ISegmentFile.SegmentSize"/>
		int SegmentSize { get; }
		/// <summary>The maximum allowed index</summary>
		int MaxIndex { get; }

		/// <summary>Returns if the specified segment is valid</summary>
		/// <param name="index">The index of the segment</param>
		bool IsValid(int index);

		/// <summary>Gets the segment at specified index</summary>
		/// <param name="index">The index of the segment</param>
		byte[] GetSegmentBytes(int index);

		/// <summary>Sets a segment at specified index</summary>
		/// <param name="index">The index of the segment</param>
		/// <param name="buffer">The buffer to copy from</param>
		void SetSegmentBytes(int index, byte[] buffer);

		/// <summary>Converts a relative segment offset to a absolute blob offset</summary>
		/// <param name="index">The index of the segment</param>
		/// <param name="offset">The relative offset within the segment</param>
		/// <returns>The absolute offset</returns>
		int SegmentToAbsoluteOffset(int index, int offset) => FirstSegmentOffset + SegmentSize * index + offset;
	}
}