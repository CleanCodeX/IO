using App.Commons.Extensions;
using System;
using System.ComponentModel.DataAnnotations;
using Res = SramCommons.Properties.Resources;

namespace SramCommons.Exceptions
{
	public enum SramError
	{
        [Display(Name = nameof(Res.InvalidSramSize), ResourceType = typeof(Res))]
		InvalidSize,
        [Display(Name = nameof(Res.NoValidSramGames), ResourceType = typeof(Res))]
		NoValidGames
	}

	public class InvalidSramFileException : Exception
	{
		public InvalidSramFileException(SramError error) : base(error.GetDisplayName()) => Error = error;

		public SramError Error { get; }
	}
}

