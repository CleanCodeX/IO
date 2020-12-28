using Common.Shared.Min.Helpers;
using SramCommons.Exceptions;
using SramCommons.Helpers;
using System;
using System.IO;
using System.Runtime.InteropServices;
using SramCommons.Extensions;

namespace SramCommons.Models
{
	public class SramFile<TSram, TSaveSlot> : SramFile, ISramFile<TSaveSlot>
		where TSram : struct
		where TSaveSlot : struct
	{
		public virtual TSram Sram { get; }

		/// <summary>
		/// Creates an instance of SramFile and loads content from stream into SramBuffer and Sram strcuture
		/// </summary>
		/// <param name="stream">The stream the buffers will be loaded from</param>
		/// <param name="firstSlotOffset">The offset of first save slot in sram buffer</param>
		/// <param name="maxSlotIndex">The maximum (zero based) index of save slots the sram file can contain</param>
		public SramFile(Stream stream, int firstSlotOffset, int maxSlotIndex) : base(Marshal.SizeOf<TSram>(), Marshal.SizeOf<TSaveSlot>(), firstSlotOffset, maxSlotIndex)
		{
			// ReSharper disable once VirtualMemberCallInConstructor
			Load(stream);
			Sram = GetStructFromBuffer<TSram>(SramBuffer);
		}

		/// <summary>Gets the save slot at specified save slot index</summary>
		/// <param name="slotIndex">The (zero based) save slot index the save slot should be loaded from</param>
		/// <returns>Deserialized save slot structure at specified save slot index</returns>
		public virtual TSaveSlot GetSaveSlot(int slotIndex)
		{
			var buffer = GetSaveSlotBytes(slotIndex);
			return GetStructFromBuffer<TSaveSlot>(buffer);
		}

		/// <summary>Sets the save slot at specified save slot index</summary>
		/// <param name="slotIndex">The (zero based) save slot index the save slot should be set to</param>
		/// <param name="slot">The save slot structure to be set</param>
		public virtual void SetSaveSlot(int slotIndex, TSaveSlot slot)
		{
			var buffer = slot.ToBytesHostEndian();

			SetSaveSlotBytes(slotIndex, buffer);
		}

		private static T GetStructFromBuffer<T>(byte[] buffer)
			where T : struct => Serializer.Deserialize<T>(buffer);
	}

	public class SramFile : ISramFile, IRawSave
	{
#nullable disable
		public byte[] SramBuffer { get; protected set; } = Array.Empty<byte>();
#nullable restore

		public int FirstSaveSlotOffset { get; }
		public int SramSize { get; }
		public int SaveSlotSize { get; }

		/// <summary>Max save slot index in SRAM file</summary>
		public int MaxSaveSlotIndex { get; }
		/// <summary>Gets or sets if the save slot has been modified since last save or load</summary>
		public bool IsModified { get; set; }

		// ReSharper disable once VirtualMemberCallInConstructor
		public SramFile(Stream stream, int sramSize, int slotSize, int firstSlotOffset, int maxSlotIndex) : this(sramSize, slotSize, firstSlotOffset, maxSlotIndex) => Load(stream);
		public SramFile(int sramSize, int slotSize, int firstSlotOffset, int maxSlotIndex) => (SramSize, SaveSlotSize, FirstSaveSlotOffset, MaxSaveSlotIndex) = (sramSize, slotSize, firstSlotOffset, maxSlotIndex);

		/// <summary>Overridable method to indicate that the SRAM file in valid state. Default is true.</summary>
		/// <returns>base implementation returns always <langword>true</langword></returns>
		public virtual bool IsValid() => true;
		
		/// <summary>Checks whether a save slot index itself is valid. Can be overwritten for more checks.</summary>
		/// <param name="slotIndex">The save slot index to be checked</param>
		/// <returns>true if the save slot index itself is valid</returns>
		public virtual bool IsValid(int slotIndex) => IsValidIndex(slotIndex);

		private bool IsValidIndex(int slotIndex) => slotIndex >= 0 && slotIndex <= MaxSaveSlotIndex;

		/// <summary>Loads whole SramBuffer from stream</summary>
		/// <param name="stream">The stream to be loaded from</param>
		public virtual void Load(Stream stream)
		{
			Requires.NotNull(stream, nameof(stream));

			stream.Seek(0, SeekOrigin.End);

			if (stream.Position != SramSize)
				throw new InvalidSramFileException(SramError.InvalidSize);

			var sram = new byte[SramSize];

			stream.Position = 0;
			stream.Read(sram, 0, SramSize);

			SramBuffer = sram;
		}

		/// <summary>Gets a save slot's buffer as byte array</summary>
		/// <param name="slotIndex">The save slot index the save slot area should be loaded from</param>
		/// <returns>A byte array of the specified save slot area</returns>
		public virtual byte[] GetSaveSlotBytes(int slotIndex)
		{
			Requires.True(IsValidIndex(slotIndex), nameof(slotIndex));

			var startOffset = FirstSaveSlotOffset + slotIndex * SaveSlotSize;
			var endOffset = startOffset + SaveSlotSize;

			return SramBuffer[startOffset..endOffset];
		}

		/// <summary>Sets the save slot's buffer from byte array</summary>
		/// <param name="slotIndex">The save slot index the save slot area should be saved to</param>
		/// <param name="buffer">The buffer to be saved</param>
		public virtual void SetSaveSlotBytes(int slotIndex, byte[] buffer)
		{
			Requires.True(IsValidIndex(slotIndex), nameof(slotIndex));

			var startOffset = FirstSaveSlotOffset + slotIndex * SaveSlotSize;

			Array.Copy(buffer, 0, SramBuffer, startOffset, buffer.Length);
		}

		/// <summary>Saves whole SramBuffer to stream</summary>
		/// <param name="stream">The stream to be saved to</param>
		public virtual void Save(Stream stream) => RawSave(stream);

		protected virtual void OnRawSave() {}

		/// <summary>Saves whole SramBuffer to stream</summary>
		/// <param name="stream">The stream to be saved to</param>
		public void RawSave(Stream stream)
		{
			Requires.NotNull(stream, nameof(stream));

			OnRawSave();

			stream.Position = 0;
			stream.Write(SramBuffer, 0, SramSize);

			if (stream.Position != SramSize)
				throw new InvalidSramFileException(SramError.InvalidSize);

			IsModified = false;
		}
	}
}