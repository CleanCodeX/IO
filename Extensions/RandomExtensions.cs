using System;
using System.Diagnostics.CodeAnalysis;

namespace IO.Extensions
{
	public static class RandomExtensions
	{
		/// <summary>Returns a random integer that is within a specified range.</summary>
		/// <param name="rand"></param>
		/// <param name="minValue">The inclusive lower bound of the random number returned.</param>
		/// <param name="maxValue">The inclusive upper bound of the random number returned. <paramref name="maxValue" /> must be greater than or equal to <paramref name="minValue" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		/// <paramref name="minValue" /> is greater than <paramref name="maxValue" />.</exception>
		/// <returns>A 32-bit signed integer greater than or equal to <paramref name="minValue" /> and less than <paramref name="maxValue" />; that is, the range of return values includes <paramref name="minValue" /> but not <paramref name="maxValue" />. If <paramref name="minValue" /> equals <paramref name="maxValue" />, <paramref name="minValue" /> is returned.</returns>
		public static int NextInclusive([NotNull] this Random rand, int minValue, int maxValue) =>
			rand.Next(minValue, maxValue + 1);
	}
}
