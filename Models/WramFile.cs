using System.IO;
using System.Runtime.InteropServices;

// ReSharper disable VirtualMemberCallInConstructor

namespace SramCommons.Models
{
	/// <summary>Provides load and save functionality for a generic W-RAM file</summary>
	/// <typeparam name="TWram">The file's W-RAM type</typeparam>
	/// <typeparam name="TSramData">The file's S-RAM data segment type</typeparam> 
	public class WramFile<TWram, TSramData> : SegmentFile<TWram, TSramData>
		where TWram : struct
		where TSramData : struct
	{
		/// <summary>
		/// Creates an instance of <see c="WramFile{TSram,TSramData}"/> and loads content from stream into buffer and W-RAM structure
		/// </summary>
		/// <param name="buffer">The buffer which will be copied</param>
		/// <param name="segmentOffset">The offset of first save slot in sram buffer</param>
		public WramFile(byte[] buffer, int segmentOffset) : base(buffer, segmentOffset) { }

		/// <summary>
		/// Creates an instance of <see c="WramFile{TSram,TSramData}"/> and loads content from stream into buffer and W-RAM structure
		/// </summary>
		/// <param name="stream">The stream the buffers will be loaded from</param>
		/// <param name="segmentOffset">The offset of first save slot in sram buffer</param>
		public WramFile(Stream stream, int segmentOffset) : base(stream, segmentOffset) { }

		/// <summary>
		/// Creates an instance of <see c="WramFile{TSram,TSramData}"/> and loads content from stream into buffer and W-RAM structure
		/// </summary>
		/// <param name="segmentOffset">The offset of first save slot in sram buffer</param>
		public WramFile(int segmentOffset) : base(Marshal.SizeOf<TWram>(), segmentOffset) { }
	}

	/// <summary>Provides load and save functionality for a partial generic W-RAM file</summary>
	/// /// <typeparam name="TSramData">The file's S-RAM data segment type</typeparam> 
	public class WramFile<TSramData> : SegmentFile<TSramData>
		where TSramData : struct
	{
		/// <summary>
		/// Creates an instance of <see c="WramFile{TSramData}"/> and loads content from stream into buffer and W-RAM structure
		/// </summary>
		/// <param name="stream">The stream the buffers will be loaded from</param>
		/// <param name="segmentSize">The size of the segment</param>
		/// <param name="segmentOffset">The offset of first save slot in sram buffer</param>
		public WramFile(Stream stream, int segmentSize, int segmentOffset) : base(stream, segmentSize, segmentOffset) { }

		/// <summary>
		/// Creates an instance of <see c="WramFile{TSramData}"/> and loads content from stream into buffer and W-RAM structure
		/// </summary>
		/// <param name="buffer">The buffer which will be copied</param>
		/// /// <param name="segmentSize">The size of the segment</param>
		/// <param name="segmentOffset">The offset of first save slot in sram buffer</param>
		public WramFile(byte[] buffer, int segmentSize, int segmentOffset) : base(buffer, segmentSize, segmentOffset) { }

		/// <summary>
		/// Creates an instance of <see c="WramFile{TSramData}"/> and loads content from stream into buffer and W-RAM structure
		/// </summary>
		/// <param name="size">The size of the buffer to create</param>
		/// <param name="segmentSize">The size of the segment</param>
		/// <param name="segmentOffset">The offset of first save slot in sram buffer</param>
		public WramFile(int size, int segmentSize, int segmentOffset) : base(size, segmentSize, segmentOffset) { }
	}

	/// <summary>Provides load and save functionality for a non-generic W-RAM file</summary>
	public class WramFile : SegmentFile
	{
		/// <summary>
		/// Creates an instance of <see c="WramFile"/> and loads content from stream into buffer and W-RAM structure
		/// </summary>
		/// <param name="stream">The stream the buffers will be loaded from</param>
		/// <param name="segmentSize">The size of the segment</param>
		/// <param name="segmentOffset">The offset of first save slot in sram buffer</param>
		public WramFile(Stream stream, int segmentSize, int segmentOffset) : base(stream, segmentSize, segmentOffset) { }

		/// <summary>
		/// Creates an instance of <see c="WramFile"/> and loads content from stream into buffer and W-RAM structure
		/// </summary>
		/// <param name="buffer">The buffer which will be copied</param>
		/// /// <param name="segmentSize">The size of the segment</param>
		/// <param name="segmentOffset">The offset of first save slot in sram buffer</param>
		public WramFile(byte[] buffer, int segmentSize, int segmentOffset) : base(buffer, segmentSize, segmentOffset) { }

		/// <summary>
		/// Creates an instance of <see c="WramFile"/> and loads content from stream into buffer and W-RAM structure
		/// </summary>
		/// <param name="size">The size of the buffer to create</param>
		/// <param name="segmentSize">The size of the segment</param>
		/// <param name="segmentOffset">The offset of first save slot in sram buffer</param>
		public WramFile(int size, int segmentSize, int segmentOffset) : base(size, segmentSize, segmentOffset) { }
	}
}