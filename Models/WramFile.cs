using System.IO;
using System.Runtime.InteropServices;

// ReSharper disable VirtualMemberCallInConstructor

namespace SramCommons.Models
{
	/// <summary>Provides load and save functionality for a generic segmented W-RAM file</summary>
	/// <typeparam name="TWram">The file's W-RAM type</typeparam>
	/// <typeparam name="TSramSegment">The file's S-RAM data segment type</typeparam> 
	public class WramFile<TWram, TSramSegment> : SegmentFile<TWram, TSramSegment>
		where TWram : struct
		where TSramSegment : struct
	{
		/// <summary>
		/// Creates an instance of <see cref="WramFile{TSram,TSramSegment}"/> and loads content from stream into buffer and W-RAM structure
		/// </summary>
		/// <param name="buffer">The buffer which will be copied</param>
		/// <param name="segmentOffset">The offset of first save slot in sram buffer</param>
		public WramFile(byte[] buffer, int segmentOffset) : base(buffer, segmentOffset) { }

		/// <summary>
		/// Creates an instance of <see cref="WramFile{TSram,TSramSegment}"/> and loads content from stream into buffer and W-RAM structure
		/// </summary>
		/// <param name="stream">The stream the buffers will be loaded from</param>
		/// <param name="segmentOffset">The offset of first save slot in sram buffer</param>
		public WramFile(Stream stream, int segmentOffset) : base(stream, segmentOffset) { }

		/// <summary>
		/// Creates an instance of <see cref="WramFile{TSram,TSramSegment}"/> and loads content from stream into buffer and W-RAM structure
		/// </summary>
		/// <param name="segmentOffset">The offset of first save slot in sram buffer</param>
		public WramFile(int segmentOffset) : base(Marshal.SizeOf<TWram>(), segmentOffset) { }
	}

	/// <summary>Provides load and save functionality for a partial generic segmented W-RAM file</summary>
	/// <typeparam name="TSramSegment">The file's S-RAM data segment type</typeparam> 
	public class WramSegmentFile<TSramSegment> : SegmentFile<TSramSegment>
		where TSramSegment : struct
	{
		/// <summary>
		/// Creates an instance of <see cref="WramFile{TSramSegment}"/> and loads content from stream into buffer and W-RAM structure
		/// </summary>
		/// <param name="stream">The stream the buffers will be loaded from</param>
		/// <param name="segmentSize">The size of the segment</param>
		/// <param name="segmentOffset">The offset of first save slot in sram buffer</param>
		public WramSegmentFile(Stream stream, int segmentSize, int segmentOffset) : base(stream, segmentSize, segmentOffset) { }

		/// <summary>
		/// Creates an instance of <see cref="WramFile{TSramSegment}"/> and loads content from stream into buffer and W-RAM structure
		/// </summary>
		/// <param name="buffer">The buffer which will be copied</param>
		/// <param name="segmentSize">The size of the segment</param>
		/// <param name="segmentOffset">The offset of first save slot in sram buffer</param>
		public WramSegmentFile(byte[] buffer, int segmentSize, int segmentOffset) : base(buffer, segmentSize, segmentOffset) { }

		/// <summary>
		/// Creates an instance of <see cref="WramFile{TSramSegment}"/> and loads content from stream into buffer and W-RAM structure
		/// </summary>
		/// <param name="size">The size of the buffer to create</param>
		/// <param name="segmentSize">The size of the segment</param>
		/// <param name="segmentOffset">The offset of first save slot in sram buffer</param>
		public WramSegmentFile(int size, int segmentSize, int segmentOffset) : base(size, segmentSize, segmentOffset) { }
	}

	/// <summary>Provides load and save functionality for a generic non-segmented W-RAM file</summary>
	/// <typeparam name="TWram">The file's W-RAM type</typeparam>
	public class WramFile<TWram> : StructFile<TWram>
		where TWram : struct
	{
		/// <summary>
		/// Creates an instance of <see cref="WramFile{TSramSegment}"/> and loads content from stream into buffer and W-RAM structure
		/// </summary>
		/// <param name="stream">The stream the buffers will be loaded from</param>
		public WramFile(Stream stream) : base(stream) { }

		/// <summary>
		/// Creates an instance of <see cref="WramFile{TSramSegment}"/> and loads content from stream into buffer and W-RAM structure
		/// </summary>
		/// <param name="buffer">The buffer which will be copied</param>
		public WramFile(byte[] buffer) : base(buffer) { }

		/// <summary>
		/// Creates an instance of <see cref="WramFile{TSramSegment}"/> and loads content from stream into buffer and W-RAM structure
		/// </summary>
		/// <param name="size">The size of the buffer to create</param>
		public WramFile(int size) : base(size) { }
	}

	/// <summary>Provides load and save functionality for a non-generic segmented W-RAM file</summary>
	public class WramSegmentFile : SegmentFile
	{
		/// <summary>
		/// Creates an instance of <see cref="WramFile"/> and loads content from stream into buffer and W-RAM structure
		/// </summary>
		/// <param name="stream">The stream the buffers will be loaded from</param>
		/// <param name="segmentSize">The size of the segment</param>
		/// <param name="segmentOffset">The offset of first save slot in sram buffer</param>
		public WramSegmentFile(Stream stream, int segmentSize, int segmentOffset) : base(stream, segmentSize, segmentOffset) { }

		/// <summary>
		/// Creates an instance of <see cref="WramFile"/> and loads content from stream into buffer and W-RAM structure
		/// </summary>
		/// <param name="buffer">The buffer which will be copied</param>
		/// <param name="segmentSize">The size of the segment</param>
		/// <param name="segmentOffset">The offset of first save slot in sram buffer</param>
		public WramSegmentFile(byte[] buffer, int segmentSize, int segmentOffset) : base(buffer, segmentSize, segmentOffset) { }

		/// <summary>
		/// Creates an instance of <see cref="WramFile"/> and loads content from stream into buffer and W-RAM structure
		/// </summary>
		/// <param name="size">The size of the buffer to create</param>
		/// <param name="segmentSize">The size of the segment</param>
		/// <param name="segmentOffset">The offset of first save slot in sram buffer</param>
		public WramSegmentFile(int size, int segmentSize, int segmentOffset) : base(size, segmentSize, segmentOffset) { }
	}

	/// <summary>Provides load and save functionality for a non-generic non-segmented W-RAM file</summary>
	public class WramFile : BlobFile
	{
		/// <summary>
		/// Creates an instance of <see cref="WramFile"/> and loads content from stream into buffer and W-RAM structure
		/// </summary>
		/// <param name="stream">The stream the buffers will be loaded from</param>
		public WramFile(Stream stream) : base(stream) { }

		/// <summary>
		/// Creates an instance of <see cref="WramFile"/> and loads content from stream into buffer and W-RAM structure
		/// </summary>
		/// <param name="buffer">The buffer which will be copied</param>
		public WramFile(byte[] buffer) : base(buffer) { }

		/// <summary>
		/// Creates an instance of <see cref="WramFile"/> and loads content from stream into buffer and W-RAM structure
		/// </summary>
		/// <param name="size">The size of the buffer to create</param>
		public WramFile(int size) : base(size) { }
	}
}