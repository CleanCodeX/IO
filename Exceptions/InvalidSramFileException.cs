using System;
using Common.Shared.Min.Attributes;
using Common.Shared.Min.Extensions;
using Res = SramCommons.Properties.Resources;

namespace SramCommons.Exceptions
{
	public enum SramError
	{
		[DisplayNameLocalized(nameof(Res.InvalidSramSize), typeof(Res))]
		InvalidSize,
		[DisplayNameLocalized(nameof(Res.NoValidGames), typeof(Res))]
		NoValidGames
	}

	public class InvalidSramFileException : Exception
	{
		public InvalidSramFileException(SramError error) : base(error.GetDisplayName()) => Error = error;

		public SramError Error { get; }
	}
}

