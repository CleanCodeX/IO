namespace IO.Models
{
	/// <summary>
	/// Provides copy get struct functionality
	/// </summary>
	/// <inheritdoc cref="IStructFile"/>
	public interface IStructFile<TStruct> : IStructFile
		where TStruct : struct
	{
		TStruct Struct { get; }
	}

	/// <summary>
	/// Provides copy functionality from buffer to struct and vice versa
	/// </summary>
	public interface IStructFile : IBlobFile
	{
		/// <summary>Copies the entire content from struct to buffer</summary>
		void CopyStructToBuffer();

		/// <summary>Copies the entire content from buffer to struct</summary>
		void CopyBufferToStruct();

		/// <summary>Whether the struct is modified or not</summary>
		bool StructIsModified { get; }
	}
}