using System.IO;
using System.Runtime.InteropServices;
using IO.Helpers;
// ReSharper disable VirtualMemberCallInConstructor

namespace IO.Models
{
	/// <summary>Provides load and save functionality for a generic struct file</summary>
	/// <typeparam name="TStruct">The file's structure type</typeparam>
	public class StructFile<TStruct> : BlobFile
		where TStruct : struct
	{
		private bool SaveStruct { get; set; }

		private TStruct _struct;

		/// <summary>The typed struct</summary>
		public virtual TStruct Struct
		{
			get => _struct;
			protected set
			{
				_struct = value;
				SaveStruct = true;
			}
		}

		/// <summary>
		/// Creates an instance of <see cref="StructFile{TStruct}"/> and loads content from buffer into buffer and struct
		/// </summary>
		/// <param name="buffer">The buffer which will be copied</param>
		public StructFile(byte[] buffer) : this() => Load(buffer);

		/// <summary>
		/// Creates an instance of <see cref="StructFile{TStruct}"/> and loads content from stream into buffer and struct
		/// </summary>
		/// <param name="stream">The stream the buffers will be loaded from</param>
		public StructFile(Stream stream) : this() => Load(stream);

		/// <summary>
		/// Creates an instance of <see cref="StructFile{TStruct}"/> and loads content from stream into buffer and struct
		/// </summary>
		public StructFile(int size) : base(size) { }

		/// <summary>
		/// Creates an instance of <see cref="StructFile{TStruct}"/> and loads content from stream into buffer and struct
		/// </summary>
		public StructFile() : base(Marshal.SizeOf<TStruct>()) {}

		/// <inheritdoc cref="IBlobFile.Load"/>
		public override void Load(byte[] buffer)
		{
			base.Load(buffer);
			GetStructFromBlob();
			SaveStruct = false;
		}

		/// <inheritdoc cref="IBlobFile.Load"/>
		public override void Load(Stream stream)
		{
			base.Load(stream);
			GetStructFromBlob();
			SaveStruct = false;
		}

		/// <inheritdoc cref="IBlobFile.Save"/>
		public override void Save(Stream stream)
		{
			if(SaveStruct)
				SaveStructToBlob();
			base.Save(stream);
		}

		private void SaveStructToBlob() => Buffer = StructSerializer.Serialize(Struct);
		private void GetStructFromBlob() => Struct = StructSerializer.Deserialize<TStruct>(Buffer);

		/// <summary>Converts a blob into struct</summary>
		/// <typeparam name="T">The type of the struct</typeparam>
		/// <param name="buffer">The buffer to convert from</param>
		/// <returns>The converted <typeparamref name="T"/>/>></returns>
		protected static T GetStructFromBlob<T>(byte[] buffer)
			where T : struct => StructSerializer.Deserialize<T>(buffer);
	}
}