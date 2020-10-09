using System.IO;

namespace SramCommons.Models
{
	public interface ISramFile<TSramGame> : ISramFile
		where TSramGame : struct
	{
		TSramGame GetGame(int gameIndex);
		void SetGame(int gameIndex, TSramGame game);
	}

	public interface ISramFile
	{
		int MaxGameIndex { get; }

		bool IsValid();
		bool IsValid(int gameIndex);

		byte[] SramBuffer { get; }
		byte[] GetGameBytes(int gameIndex);
		void SetGameBytes(int gameIndex, byte[] buffer);

		void Load(Stream stream);
		void Save(Stream stream);
	}
}