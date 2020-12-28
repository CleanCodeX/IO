using System.IO;

namespace SramCommons.Models
{
	/// <summary>Generic interface for SRAM file</summary>
	/// <typeparam name="TSaveSlot">The SRAM's save slot structure type</typeparam>
	public interface ISramFile<TSaveSlot> : ISramFile
		where TSaveSlot : struct
	{
		/// <summary>Gets the save slot structure at specified save slot index</summary>
		TSaveSlot GetSaveSlot(int slotIndex);

		/// <summary>Sets a save slot structure at specified save slot index</summary>
		void SetSaveSlot(int slotIndex, TSaveSlot slot);
	}

	/// <summary>Non generic interface for SRAM file</summary>
	public interface ISramFile
	{
		int FirstSaveSlotOffset { get; }
		int SramSize { get; }
		int SaveSlotSize { get; }
		int MaxSaveSlotIndex { get; }

		/// <summary>Returns if the specified sram file is valid, if implemented</summary>
		bool IsValid();

		/// <summary>Returns if a save slot index itself is valid and (if implemented) if the specified save slot is valid</summary>
		bool IsValid(int slotIndex);

		/// <summary>The buffer of the whole SRAM file</summary>
		byte[] SramBuffer { get; }

		/// <summary>Gets the save slot area at specified save slot index</summary>
		byte[] GetSaveSlotBytes(int slotIndex);

		/// <summary>Sets a save slot area at specified save slot index</summary>
		void SetSaveSlotBytes(int slotIndex, byte[] buffer);

		/// <summary>Loads SRAM buffer from stream</summary>
		void Load(Stream stream);

		/// <summary>Saves SRAM buffer to stream</summary>
		void Save(Stream stream);

		int SaveSlotToSramOffset(int slotIndex, int offset) => FirstSaveSlotOffset + SaveSlotSize * slotIndex + offset;
	}
}