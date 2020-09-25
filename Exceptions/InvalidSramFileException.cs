using System;

namespace SramCommons.Exceptions
{
	/// The possible error codes
	public enum FileError
	{
		InvalidSize,
		NoValidGames
	}

	/**
	 * Exception thrown when a file is not a valid SRAM file.
	 */
	public class InvalidSramFileException : Exception
	{
		public InvalidSramFileException(FileError error) : base(nameof(InvalidSramFileException)) => Error = error;

		public FileError Error { get; }
	}
}

