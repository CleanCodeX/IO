using System.IO;

namespace SramCommons.Models
{
	public interface ISramFile<out TSramGame> : ISramFile
		where TSramGame : struct
	{
		TSramGame GetGame(int gameIndex);
	}

	public interface ISramFile
	{
		int MaxGameIndex { get; }

		bool IsValid();
		bool IsValid(int gameIndex);

		byte[] SramBuffer { get; }
		byte[] GetGameBytes(int gameIndex);

		void Load(Stream stream);
		void Save(Stream stream);
	}
}