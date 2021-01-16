using System.IO;

namespace SramCommons.Models
{
	/// <summary>Non generic interface for blob file</summary>
	public interface IBlobFile
	{
		/// <summary>The size of the blob</summary>
		int Size { get; }

		/// <summary>Returns if the buffer is valid, if implemented</summary>
		bool IsValid();

		/// <summary>The buffer of the blob file</summary>
		byte[] Buffer { get; }

		/// <summary>Loads blob from stream</summary>
		void Load(Stream stream);

		/// <summary>Saves blob to stream</summary>
		void Save(Stream stream);
	}
}