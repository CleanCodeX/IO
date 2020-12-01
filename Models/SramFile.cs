using Common.Shared.Min.Helpers;
using SramCommons.Exceptions;
using SramCommons.Helpers;
using System;
using System.IO;
using System.Runtime.InteropServices;
using SramCommons.Extensions;

namespace SramCommons.Models
{
	public class SramFile<TSram, TSramGame> : SramFile, ISramFile<TSramGame>
		where TSram : struct
		where TSramGame : struct
	{
		public virtual TSram Sram { get; }

		/// <summary>
		/// Creates an instance of SramFile and loads content from stream into SramBuffer and Sram strcuture
		/// </summary>
		/// <param name="stream">The stream the buffers will be loaded from</param>
		/// <param name="firstGameOffset">The offset of first game in sram buffer</param>
		/// <param name="maxGameIndex">The maximum (zero based) index of games the sram file can contain</param>
		public SramFile(Stream stream, int firstGameOffset, int maxGameIndex) : base(Marshal.SizeOf<TSram>(), Marshal.SizeOf<TSramGame>(), firstGameOffset, maxGameIndex)
		{
			// ReSharper disable once VirtualMemberCallInConstructor
			Load(stream);
			Sram = GetStructFromBuffer<TSram>(SramBuffer);
		}

		/// <summary>Gets the game at specified game index</summary>
		/// <param name="gameIndex">The (zero based) game index the game should be loaded from</param>
		/// <returns>Deserialized game structure at specified game index</returns>
		public virtual TSramGame GetGame(int gameIndex)
		{
			var buffer = GetGameBytes(gameIndex);
			return GetStructFromBuffer<TSramGame>(buffer);
		}

		/// <summary>Sets the game at specified game index</summary>
		/// <param name="gameIndex">The (zero based) game index the game should be set to</param>
		/// <param name="game">The game structure to be set</param>
		public virtual void SetGame(int gameIndex, TSramGame game)
		{
			var buffer = game.ToBytesHostEndian();

			SetGameBytes(gameIndex, buffer);
		}

		private static T GetStructFromBuffer<T>(byte[] buffer)
			where T : struct => Serializer.Deserialize<T>(buffer);
	}

	public class SramFile : ISramFile, IRawSave
	{
#nullable disable
		public byte[] SramBuffer { get; protected set; } = Array.Empty<byte>();
#nullable restore

		public int FirstGameOffset { get; }
		public int SramSize { get; }
		public int GameSize { get; }

		/// <summary>Max game index in SRAM file</summary>
		public int MaxGameIndex { get; }
		/// <summary>Gets or sets if the game has been modified since last save or load</summary>
		public bool IsModified { get; set; }

		// ReSharper disable once VirtualMemberCallInConstructor
		public SramFile(Stream stream, int sramSize, int gameSize, int firstGameOffset, int maxGameIndex) : this(sramSize, gameSize, firstGameOffset, maxGameIndex) => Load(stream);
		public SramFile(int sramSize, int gameSize, int firstGameOffset, int maxGameIndex) => (SramSize, GameSize, FirstGameOffset, MaxGameIndex) = (sramSize, gameSize, firstGameOffset, maxGameIndex);

		/// <summary>Overridable method to indicate that the SRAM file in valid state. Default is true.</summary>
		/// <returns>base implementation returns always <langword>true</langword></returns>
		public virtual bool IsValid() => true;
		
		/// <summary>Checks whether a game index itself is valid. Can be overwritten for more checks.</summary>
		/// <param name="gameIndex">The game index to be checked</param>
		/// <returns>true if the game index itself is valid</returns>
		public virtual bool IsValid(int gameIndex) => IsValidIndex(gameIndex);

		private bool IsValidIndex(int gameIndex) => gameIndex >= 0 && gameIndex <= MaxGameIndex;

		/// <summary>Loads whole SramBuffer from stream</summary>
		/// <param name="stream">The stream to be loaded from</param>
		public virtual void Load(Stream stream)
		{
			Requires.NotNull(stream, nameof(stream));

			stream.Seek(0, SeekOrigin.End);

			if (stream.Position != SramSize)
				throw new InvalidSramFileException(SramError.InvalidSize);

			var sram = new byte[SramSize];

			stream.Position = 0;
			stream.Read(sram, 0, SramSize);

			SramBuffer = sram;
		}

		/// <summary>Gets a game's buffer as byte array</summary>
		/// <param name="gameIndex">The game index the game buffer should be loaded from</param>
		/// <returns>A byte array of the specified game buffer</returns>
		public virtual byte[] GetGameBytes(int gameIndex)
		{
			Requires.True(IsValidIndex(gameIndex), nameof(gameIndex));

			var startOffset = FirstGameOffset + gameIndex * GameSize;
			var endOffset = startOffset + GameSize;

			return SramBuffer[startOffset..endOffset];
		}

		/// <summary>Sets the game's buffer from byte array</summary>
		/// <param name="gameIndex">The game index the game buffer should be saved to</param>
		/// <param name="buffer">The buffer to be saved</param>
		public virtual void SetGameBytes(int gameIndex, byte[] buffer)
		{
			Requires.True(IsValidIndex(gameIndex), nameof(gameIndex));

			var startOffset = FirstGameOffset + gameIndex * GameSize;

			Array.Copy(buffer, 0, SramBuffer, startOffset, buffer.Length);
		}

		/// <summary>Saves whole SramBuffer to stream</summary>
		/// <param name="stream">The stream to be saved to</param>
		public virtual void Save(Stream stream) => RawSave(stream);

		protected virtual void OnRawSave() {}

		/// <summary>Saves whole SramBuffer to stream</summary>
		/// <param name="stream">The stream to be saved to</param>
		public void RawSave(Stream stream)
		{
			Requires.NotNull(stream, nameof(stream));

			OnRawSave();

			stream.Position = 0;
			stream.Write(SramBuffer, 0, SramSize);

			if (stream.Position != SramSize)
				throw new InvalidSramFileException(SramError.InvalidSize);

			IsModified = false;
		}
	}
}