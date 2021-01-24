using System.IO;

namespace IO.Models
{
	/// <summary>Provides access for raw save functionality</summary>
	public interface IRawSave
	{
		/// <summary>Saves the contents directly</summary>
		/// <param name="stream">The stream to save to</param>
		void RawSave(Stream stream);
	}
}