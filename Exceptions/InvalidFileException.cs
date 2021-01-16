using System;
using Res = SramCommons.Properties.Resources;

namespace SramCommons.Exceptions
{
	public class InvalidFileException : Exception
	{
		public InvalidFileException() : base(Res.ErrorInvalidSize) {}
	}
}

