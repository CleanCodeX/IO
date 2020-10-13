using System.IO;

namespace SramCommons.Models
{
	/// <summary>Generic interface for SRAM file</summary>
	/// <typeparam name="TSramGame">The SRAM's game structure type</typeparam>
	public interface ISramFile<TSramGame> : ISramFile
		where TSramGame : struct
	{
		/// <summary>Gets the game structure at specified game index</summary>
		TSramGame GetGame(int gameIndex);

		/// <summary>Sets a game structure at specified game index</summary>
		void SetGame(int gameIndex, TSramGame game);
	}

	/// <summary>Non generic interface for SRAM file</summary>
	public interface ISramFile
	{
		int MaxGameIndex { get; }

		/// <summary>Returns if the specified sram file is valid, if implemented</summary>
		bool IsValid();

		/// <summary>Returns if a game index itself is valid and (if implemented) if the specified game is valid</summary>
		bool IsValid(int gameIndex);

		/// <summary>The buffer of the whole SRAM file</summary>
		byte[] SramBuffer { get; }

		/// <summary>Gets the game buffer at specified game index</summary>
		byte[] GetGameBytes(int gameIndex);

		/// <summary>Sets a game buffer at specified game index</summary>
		void SetGameBytes(int gameIndex, byte[] buffer);

		/// <summary>Loads SRAM buffer from stream</summary>
		void Load(Stream stream);

		/// <summary>Saves SRAM buffer to stream</summary>
		void Save(Stream stream);
	}
}