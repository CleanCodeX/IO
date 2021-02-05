using System;
using System.Diagnostics.CodeAnalysis;

namespace IO.Services
{
	public interface IArrayFormatter
	{
		/// <summary>
		/// Formates the array.
		/// </summary>
		/// <param name="source"></param>
		/// <param name="options">The options to be used.</param>
		/// <returns>A formatted string</returns>
		string Format([NotNull] in Array source, ArrayFormattingOptions? options = default);
	}
}
