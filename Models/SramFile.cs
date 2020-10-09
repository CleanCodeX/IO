using Common.Shared.Min.Helpers;
using SramCommons.Exceptions;
using SramCommons.Helpers;
using System;
using System.IO;
using System.Runtime.InteropServices;
using SramCommons.Extensions;

namespace SramCommons.Models
{
	public static class SramFileExtensions
	{
		public static void Load(this ISramFile source, string filepath)
		{
			Requires.FileExists(filepath, nameof(filepath));

			source.Load(new FileStream(filepath, FileMode.Open));
		}

		public static void Save(this ISramFile source, string filepath)
		{
			Requires.NotNullOrEmpty(filepath, nameof(filepath));

			source.Save(new FileStream(filepath, FileMode.Create));
		}
	}

	public class SramFile<TSram, TSramGame> : SramFile, ISramFile<TSramGame>
		where TSram : struct
		where TSramGame : struct
	{
		public virtual TSram Sram { get; }

		public SramFile(Stream stream, int firstGameOffset, int maxGameIndex) : base(Marshal.SizeOf<TSram>(), Marshal.SizeOf<TSramGame>(), firstGameOffset, maxGameIndex)
		{
			// ReSharper disable once VirtualMemberCallInConstructor
			Load(stream);
			Sram = GetStructFromBuffer<TSram>(SramBuffer);
		}

		public virtual TSramGame GetGame(int gameIndex)
		{
			var buffer = GetGameBytes(gameIndex);
			return GetStructFromBuffer<TSramGame>(buffer);
		}

		public virtual void SetGame(int gameIndex, TSramGame game)
		{
			var buffer = game.ToBytesHostEndian();

			SetGameBytes(gameIndex, buffer);
		}

		private static T GetStructFromBuffer<T>(byte[] buffer)
			where T : struct => Serializer.Deserialize<T>(buffer);
	}

	public class SramFile : ISramFile
	{
#nullable disable
		public byte[] SramBuffer { get; protected set; } = Array.Empty<byte>();
#nullable restore

		protected int FirstGameOffset { get; }
		protected int SramSize { get; }
		protected int GameSize { get; }

		public int MaxGameIndex { get; }
		public bool IsModified { get; set; }

		// ReSharper disable once VirtualMemberCallInConstructor
		public SramFile(Stream stream, int sramSize, int gameSize, int firstGameOffset, int maxGameIndex) : this(sramSize, gameSize, firstGameOffset, maxGameIndex) => Load(stream);
		public SramFile(int sramSize, int gameSize, int firstGameOffset, int maxGameIndex) => (SramSize, GameSize, FirstGameOffset, MaxGameIndex) = (sramSize, gameSize, firstGameOffset, maxGameIndex);

		public virtual bool IsValid() => true;
		public virtual bool IsValid(int gameIndex) => IsValidIndex(gameIndex);
		private bool IsValidIndex(int gameIndex) => gameIndex >= 0 && gameIndex <= MaxGameIndex;

		public virtual void Load(Stream stream)
		{
			Requires.NotNull(stream, nameof(stream));

			stream.Seek(0, SeekOrigin.End);

			if (stream.Position != SramSize)
				throw new InvalidSramFileException(SramError.InvalidSize);

			var sram = new byte[SramSize];

			stream.Seek(0, SeekOrigin.Begin);
			stream.Read(sram, 0, SramSize);
			stream.Close();

			SramBuffer = sram;
		}

		public virtual byte[] GetGameBytes(int gameIndex)
		{
			Requires.True(IsValidIndex(gameIndex), nameof(gameIndex));

			var startOffset = FirstGameOffset + gameIndex * GameSize;
			var endOffset = startOffset + GameSize;

			return SramBuffer[startOffset..endOffset];
		}

		public virtual void SetGameBytes(int gameIndex, byte[] buffer)
		{
			Requires.True(IsValidIndex(gameIndex), nameof(gameIndex));

			var startOffset = FirstGameOffset + gameIndex * GameSize;

			Array.Copy(buffer, 0, SramBuffer, startOffset, buffer.Length);
		}

		public virtual void Save(Stream stream)
		{
			Requires.NotNull(stream, nameof(stream));

			stream.Position = 0;
			stream.Write(SramBuffer, 0, SramSize);

			if (stream.Position != SramSize)
				throw new InvalidSramFileException(SramError.InvalidSize);

			stream.Close();
			IsModified = false;
		}
	}
}