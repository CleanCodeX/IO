using System.IO;

namespace SramCommons.Models
{
	public interface IRawSave
	{
		void RawSave(Stream stream);
	}
}