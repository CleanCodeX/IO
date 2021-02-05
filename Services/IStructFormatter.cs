namespace IO.Services
{
	public interface IStructFormatter
	{
		/// <summary>
		/// Formates the structure.
		/// </summary>
		/// <param name="source"></param>
		/// <param name="options">The options to be used.</param>
		/// <returns>A formatted string</returns>
		string Format<T>(T source, StructFormattingOptions? options = default) where T : struct;
	}
}
