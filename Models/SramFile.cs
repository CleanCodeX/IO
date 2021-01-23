using System.IO;
using System.Runtime.InteropServices;

// ReSharper disable VirtualMemberCallInConstructor

namespace SramCommons.Models
{
	/// <summary>Provides load and save functionality for a generic segmented S-RAM file</summary>
	/// <typeparam name="TSram">The file's S-RAM type</typeparam>
	/// <typeparam name="TSaveSlot">The file's S-RAM segment type</typeparam> 
	public class SramFile<TSram, TSaveSlot> : MultiSegmentFile<TSram, TSaveSlot>
		where TSram : struct
		where TSaveSlot : struct
	{
		/// <summary>
		/// Creates an instance of <see cref="SramFile{TSram,TSaveSlot}"/> and loads content from stream into buffer and S-RAM structure
		/// </summary>
		/// <param name="buffer">The buffer which will be copied</param>
		/// <param name="firstSegmentOffset">The offset of first save slot in sram buffer</param>
		/// <param name="maxIndex">The maximum (zero based) index of save slots the sram file can contain</param>
		public SramFile(byte[] buffer, int firstSegmentOffset, int maxIndex) : base(buffer, firstSegmentOffset, maxIndex) {}

		/// <summary>
		/// Creates an instance of <see cref="SramFile{TSram,TSaveSlot}"/> and loads content from stream into buffer and S-RAM structure
		/// </summary>
		/// <param name="stream">The stream the buffers will be loaded from</param>
		/// <param name="firstSegmentOffset">The offset of first save slot in sram buffer</param>
		/// <param name="maxIndex">The maximum (zero based) index of save slots the sram file can contain</param>
		public SramFile(Stream stream, int firstSegmentOffset, int maxIndex) : base(stream, firstSegmentOffset, maxIndex) { }

		/// <summary>
		/// Creates an instance of <see cref="SramFile{TSram,TSaveSlot}"/> and loads content from stream into buffer and S-RAM structure
		/// </summary>
		/// <param name="firstSegmentOffset">The offset of first save slot in sram buffer</param>
		/// <param name="maxIndex">The maximum (zero based) index of save slots the sram file can contain</param>
		public SramFile(int firstSegmentOffset, int maxIndex) : base(Marshal.SizeOf<TSram>(), firstSegmentOffset, maxIndex) { }
	}

	/// <summary>Provides load and save functionality for a generic non-segmented S-RAM file</summary>
	/// <typeparam name="TSram">The file's S-RAM type</typeparam>
	public class SramFile<TSram> : StructFile<TSram>
		where TSram : struct
	{
		/// <summary>
		/// Creates an instance of <see cref="SramFile{TSram}"/> and loads content from stream into buffer and S-RAM structure
		/// </summary>
		/// <param name="buffer">The buffer which will be copied</param>
		public SramFile(byte[] buffer) : base(buffer) { }

		/// <summary>
		/// Creates an instance of <see cref="SramFile{TSram}"/> and loads content from stream into buffer and S-RAM structure
		/// </summary>
		/// <param name="stream">The stream the buffers will be loaded from</param>
		public SramFile(Stream stream) : base(stream) { }

		/// <summary>
		/// Creates an instance of <see cref="SramFile{TSram}"/> and loads content from stream into buffer and S-RAM structure
		/// </summary>
		public SramFile() : base(Marshal.SizeOf<TSram>()) { }
	}

	/// <summary>Provides load and save functionality for a non-generic segmented S-RAM file</summary>
	public class SramFile : MultiSegmentFile
	{
		/// <summary>
		/// Creates an instance of <see cref="SramFile"/> and loads content from stream into buffer and S-RAM structure
		/// </summary>
		/// <param name="stream">The stream the buffers will be loaded from</param>
		/// <param name="segmentSize">The size of the segment</param>
		/// <param name="firstSegmentOffset">The offset of first save slot in sram buffer</param>
		/// <param name="maxIndex">The maximum (zero based) index of save slots the sram file can contain</param>
		public SramFile(Stream stream, int segmentSize, int firstSegmentOffset, int maxIndex) : base(stream, (int)stream.Length, segmentSize, firstSegmentOffset, maxIndex) { }

		/// <summary>
		/// Creates an instance of <see cref="SramFile"/> and loads content from stream into buffer and S-RAM structure
		/// </summary>
		/// <param name="buffer">The buffer which will be copied</param>
		/// /// <param name="segmentSize">The size of the segment</param>
		/// <param name="firstSegmentOffset">The offset of first save slot in sram buffer</param>
		/// <param name="maxIndex">The maximum (zero based) index of save slots the sram file can contain</param>
		public SramFile(byte[] buffer, int segmentSize, int firstSegmentOffset, int maxIndex) : base(buffer, buffer.Length, segmentSize, firstSegmentOffset, maxIndex) { }

		/// <summary>
		/// Creates an instance of <see cref="SramFile"/> and loads content from stream into buffer and S-RAM structure
		/// </summary>
		/// <param name="size">The size of the buffer to create</param>
		/// <param name="segmentSize">The size of the segment</param>
		/// <param name="firstSegmentOffset">The offset of first save slot in sram buffer</param>
		/// <param name="maxIndex">The maximum (zero based) index of save slots the sram file can contain</param>
		public SramFile(int size, int segmentSize, int firstSegmentOffset, int maxIndex) : base(size, segmentSize,
			firstSegmentOffset, maxIndex) {}
	}
}