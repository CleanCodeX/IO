namespace IO.Models
{
	/// <summary>Generic interface for segment blob file</summary>
	/// <typeparam name="TSegment">The segment's structure type</typeparam>
	public interface ISegmentFile<TSegment> : ISegmentFile
		where TSegment : struct
	{
		/// <summary>Gets the segment structure</summary>
		TSegment GetSegment();

		/// <summary>Sets a segment structure</summary>
		void SetSegment(TSegment segment);
	}

	/// <summary>Non generic interface for a segment blob file</summary>
	public interface ISegmentFile : IBlobFile
	{
		/// <summary>The start offset for the segment</summary>
		int SegmentOffset { get; }
		/// <summary>The size of the segment</summary>
		int SegmentSize { get; }

		/// <summary>Gets the segment bytes</summary>
		byte[] GetSegmentBytes();

		/// <summary>Sets the segment bytes</summary>
		void SetSegmentBytes(byte[] buffer);

		/// <summary>Converts a relative segment offset to a absolute blob offset</summary>
		/// <param name="offset">The relative offset within the segment</param>
		/// <returns>The absolute offset</returns>
		int SegmentToAbsoluteOffset(int offset) => SegmentOffset + offset;
	}
}