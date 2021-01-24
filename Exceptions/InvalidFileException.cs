using System;
using IO.Properties;

namespace IO.Exceptions
{
	public class InvalidFileException : Exception
	{
		public InvalidFileException() : base(Resources.ErrorInvalidSize) {}
	}
}