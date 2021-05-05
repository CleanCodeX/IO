using System;
using System.IO;
using System.Runtime.InteropServices;
using Common.Shared.Min.Helpers;
using IO.Extensions;
// ReSharper disable VirtualMemberCallInConstructor

namespace IO.Models
{
	/// <summary>Provides load and save functionality for a generic <see cref="MultiSegmentFile{TStruct,TSegment}"/></summary>
	/// <typeparam name="TStruct">The file's structure type</typeparam>
	/// <typeparam name="TSegment">The segment's structure type</typeparam> 
	public class MultiSegmentFile<TStruct, TSegment> : StructFile<TStruct>, IMultiSegmentFile<TSegment>
		where TStruct : struct
		where TSegment : struct
	{
		/// <inheritdoc cref="IMultiSegmentFile.FirstSegmentOffset"/>
		public int FirstSegmentOffset { get; }
		/// <inheritdoc cref="IMultiSegmentFile.SegmentSize"/>
		public int SegmentSize { get; }
		/// <inheritdoc cref="IMultiSegmentFile.MaxIndex"/>
		public int MaxIndex { get; }

		/// <summary>
		/// Creates an instance of <see cref="MultiSegmentFile{TStruct,TSegment}"/> and loads content from stream into buffer and S-RAM structure
		/// </summary>
		/// <param name="buffer">The buffer which will be copied</param>
		/// <param name="firstSegmentOffset">The offset of first segment in blob buffer</param>
		/// <param name="maxIndex">The maximum (zero based) index of save slots the blob file can contain</param>
		public MultiSegmentFile(byte[] buffer, int firstSegmentOffset, int maxIndex) : this(buffer.Length, firstSegmentOffset, maxIndex) => Load(buffer);

		/// <summary>
		/// Creates an instance of <see cref="MultiSegmentFile{TStruct,TSegment}"/> and loads content from stream into buffer and S-RAM structure
		/// </summary>
		/// <param name="stream">The stream the buffers will be loaded from</param>
		/// <param name="firstSegmentOffset">The offset of first segment in blob buffer</param>
		/// <param name="maxIndex">The maximum (zero based) index of save slots the blob file can contain</param>
		public MultiSegmentFile(Stream stream, int firstSegmentOffset, int maxIndex) : this((int)stream.Length,
			firstSegmentOffset, maxIndex) => Load(stream);

		/// <summary>
		/// Creates an instance of <see cref="MultiSegmentFile{TStruct,TSegment}"/> and creates a buffer of the specified size
		/// </summary>
		/// <param name="size">The size of the surrounding buffer</param>
		/// <param name="firstSegmentOffset">The offset of first segment in blob buffer</param>
		/// <param name="maxIndex">The maximum (zero based) index of save slots the blob file can contain</param>
		public MultiSegmentFile(int size, int firstSegmentOffset, int maxIndex) : base(size) => (SegmentSize, FirstSegmentOffset, MaxIndex) = (Marshal.SizeOf<TSegment>(), firstSegmentOffset, maxIndex);

		/// <inheritdoc cref="IMultiSegmentFile{TSegment}.GetSegment"/>
		public virtual TSegment GetSegment(int index) => GetStructFromBuffer<TSegment>(GetSegmentBytes(index));

		/// <inheritdoc cref="IMultiSegmentFile{TSegment}.SetSegmentBytes"/>
		public virtual void SetSegment(int index, TSegment segment) => SetSegmentBytes(index, segment.ToBytes());

		/// <inheritdoc cref="IMultiSegmentFile.GetSegmentBytes"/>
		public virtual byte[] GetSegmentBytes(int index)
		{
			Requires.True(CheckValidIndex(index), nameof(index));

			var startOffset = FirstSegmentOffset + index * SegmentSize;
			var endOffset = startOffset + SegmentSize;

			return Buffer[startOffset..endOffset];
		}

		/// <inheritdoc cref="IMultiSegmentFile.SetSegmentBytes"/>
		public virtual void SetSegmentBytes(int index, byte[] buffer)
		{
			Requires.True(CheckValidIndex(index), nameof(index));

			var startOffset = FirstSegmentOffset + index * SegmentSize;

			Array.Copy(buffer, 0, Buffer, startOffset, SegmentSize);
		}

		/// <summary>Checks whether a segment index itself is valid. Can be overwritten for further checks.</summary>
		/// <param name="index">The segment index to be checked</param>
		/// <returns>true if the segment index is valid</returns>
		public virtual bool IsValid(int index) => CheckValidIndex(index);

		private bool CheckValidIndex(int index)
		{
			if (index >= 0 && index <= MaxIndex) return true;

			throw new ArgumentException($"Invalid Index. Valid range is 0-{MaxIndex}, but was {index}.");
		}
	}

	/// <summary>Provides load and save functionality for a non-generic multi segment file</summary>
	public class MultiSegmentFile : BlobFile, IMultiSegmentFile
	{
		/// <inheritdoc cref="IMultiSegmentFile.FirstSegmentOffset"/>
		public int FirstSegmentOffset { get; }
		/// <inheritdoc cref="IMultiSegmentFile.SegmentSize"/>
		public int SegmentSize { get; }
		/// <inheritdoc cref="IMultiSegmentFile.MaxIndex"/>
		public int MaxIndex { get; }

		/// <summary>
		/// Creates an instance of <see cref="MultiSegmentFile"/> and loads content from stream into buffer and S-RAM structure
		/// </summary>
		/// <param name="stream">The stream the buffers will be loaded from</param>
		/// <param name="segmentSize">The defined size of the segment</param>
		/// <param name="firstSegmentOffset">The offset of first segment in blob buffer</param>
		/// <param name="maxIndex">The maximum (zero based) index of save slots the blob file can contain</param>
		/// <param name="size">The defined size of the buffer</param>
		public MultiSegmentFile(Stream stream, int size, int segmentSize, int firstSegmentOffset, int maxIndex) : this(size, segmentSize, firstSegmentOffset, maxIndex) => Load(stream);

		/// <summary>
		/// Creates an instance of <see cref="MultiSegmentFile"/> and loads content from stream into buffer and S-RAM structure
		/// </summary>
		/// <param name="buffer">The buffer which will be copied</param>
		/// <param name="segmentSize">The defined size of the segment</param>
		/// <param name="firstSegmentOffset">The offset of first segment in blob buffer</param>
		/// <param name="maxIndex">The maximum (zero based) index of save slots the blob file can contain</param>
		/// <param name="size">The defined size of the buffer</param>
		public MultiSegmentFile(byte[] buffer, int size, int segmentSize, int firstSegmentOffset, int maxIndex) : this(size, segmentSize, firstSegmentOffset, maxIndex) => Load(buffer);

		/// <summary>
		/// Creates an instance of <see cref="MultiSegmentFile"/> and creates a buffer of the specified size
		/// </summary>
		/// <param name="size">The size of the surrounding buffer</param>
		/// <param name="segmentSize">The defined size of the segment</param>
		/// <param name="firstSegmentOffset">The offset of first segment in blob buffer</param>
		/// <param name="maxIndex">The maximum (zero based) index of save slots the blob file can contain</param>
		public MultiSegmentFile(int size, int segmentSize, int firstSegmentOffset, int maxIndex) : base(size) => (SegmentSize, FirstSegmentOffset, MaxIndex) = (segmentSize, firstSegmentOffset, maxIndex);

		/// <summary>Checks whether a segment index itself is valid. Can be overwritten for further checks.</summary>
		/// <param name="index">The segment index to be checked</param>
		/// <returns>true if the segment index is valid</returns>
		public virtual bool IsValid(int index) => IsValidIndex(index);
		
		private bool IsValidIndex(int index) => index >= 0 && index <= MaxIndex;

		/// <inheritdoc cref="IMultiSegmentFile.GetSegmentBytes"/>
		public virtual byte[] GetSegmentBytes(int index)
		{
			Requires.True(IsValidIndex(index), nameof(index));

			var startOffset = FirstSegmentOffset + index * SegmentSize;
			var endOffset = startOffset + SegmentSize;

			return Buffer[startOffset..endOffset];
		}

		/// <inheritdoc cref="IMultiSegmentFile.SetSegmentBytes"/>
		public virtual void SetSegmentBytes(int index, byte[] buffer)
		{
			Requires.True(IsValidIndex(index), nameof(index));

			var startOffset = FirstSegmentOffset + index * SegmentSize;

			Array.Copy(buffer, 0, Buffer, startOffset, SegmentSize);
		}
	}
}