using System;
using System.IO;
using Common.Shared.Min.Extensions;
using IO.Exceptions;

// ReSharper disable VirtualMemberCallInConstructor

namespace IO.Models
{
	/// <inheritdoc cref="IBlobFile"/>
	public class BlobFile: IBlobFile, IRawSave
	{
		private byte[] _buffer = Array.Empty<byte>();

		/// <inheritdoc cref="IBlobFile.Buffer"/>
		public byte[] Buffer
		{
			get => _buffer;
			protected set
			{
				_buffer = value;
				IsModified = true;
			}
		}

		/// <inheritdoc cref="IBlobFile.Size"/>
		public int Size { get; }

		/// <summary>Gets or sets if the file has been modified since last save or load</summary>
		public virtual bool IsModified { get; set; }

		/// <summary>
		/// Creates an instance of <see cref="BlobFile"/> and loads content from stream into buffer and struct
		/// </summary>
		/// <param name="stream">The stream which will be copied from</param>
		public BlobFile(Stream stream) : this((int)stream.Length) => Load(stream);

		/// <summary>
		/// Creates an instance of <see cref="BlobFile"/> and loads content from buffer into buffer and struct
		/// </summary>
		/// <param name="buffer">The buffer which will be copied from</param>
		public BlobFile(byte[] buffer) : this(buffer.Length) => Load(buffer);

		/// <summary>
		/// Creates an instance of <see cref="BlobFile"/> and loads content from stream into buffer and struct
		/// </summary>
		/// <param name="size">The size of the buffer to create</param>
		public BlobFile(int size) => Size = size;

		/// <summary>Overridable method to indicate that the blob file is in valid state. Default is true.</summary>
		/// <returns>base implementation returns always <langword>true</langword></returns>
		public virtual bool IsValid() => Size > 0;

		/// <summary>Loads whole buffer from stream</summary>
		/// <param name="buffer">The buffer to be copied from</param>
		public virtual void Load(byte[] buffer)
		{
			buffer.ThrowIfNull(nameof(buffer));

			OnLoading();

			Buffer = new byte[buffer.Length];
			Array.Copy(buffer, Buffer, buffer.Length);
			IsModified = false;

			OnLoaded();
		}

		/// <summary>Loads whole buffer from stream</summary>
		/// <param name="stream">The stream to be loaded from</param>
		public virtual void Load(Stream stream)
		{
			stream.ThrowIfNull(nameof(stream));

			OnLoading();

			stream.Seek(0, SeekOrigin.End);

			if (stream.Position != Size)
				throw new InvalidFileException();

			var buffer = new byte[Size];

			stream.Position = 0;
			stream.Read(buffer, 0, Size);

			Buffer = buffer;
			IsModified = false;

			OnLoaded();
		}

		protected virtual void OnLoading() { }
		protected virtual void OnLoaded() { }

		/// <summary>Saves whole buffer to stream</summary>
		/// <param name="stream">The stream to be saved to</param>
		public virtual void Save(Stream stream) => RawSave(stream);

		protected virtual void OnSaving() { }
		protected virtual void OnSaved() { }

		/// <summary>Saves whole buffer to stream</summary>
		/// <param name="stream">The stream to be saved to</param>
		public void RawSave(Stream stream)
		{
			stream.ThrowIfNull(nameof(stream));

			OnSaving();

			stream.Position = 0;
			stream.Write(Buffer, 0, Size);

			if (stream.Position != Size)
				throw new InvalidFileException();

			IsModified = false;

			OnSaved();
		}
	}
}
