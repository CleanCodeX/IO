using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using App.Commons.Helpers;
using SramCommons.Exceptions;
using SramCommons.Helpers;

namespace SramCommons.Models
{
    public class SramFile<TSram, TSramGame> : SramFile, ISramFile<TSramGame>
        where TSram : struct
        where TSramGame : struct
    {
        public virtual TSram Sram { get; }

        public SramFile(string filepath, int sramSize, int gameSize, int firstGameOffset, int maxGameIndex) : this(new FileStream(filepath, FileMode.Open), sramSize, gameSize, firstGameOffset, maxGameIndex) { }
        public SramFile(Stream stream, int sramSize, int gameSize, int firstGameOffset, int maxGameIndex) : base(sramSize, gameSize, firstGameOffset, maxGameIndex)
        {
            Debug.Assert(SramSize == Marshal.SizeOf<TSram>());
            Debug.Assert(gameSize == Marshal.SizeOf<TSramGame>());

            Load(stream);
            Sram = GetStructFromBuffer<TSram>(SramBuffer);
        }

        public virtual TSramGame GetGame(int gameIndex)
        {
            var buffer = GetGameBytes(gameIndex);
            return GetStructFromBuffer<TSramGame>(buffer);
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

        public SramFile(string filepath, int sramSize, int gameSize, int firstGameOffset, int maxGameIndex) : this(sramSize, gameSize, firstGameOffset, maxGameIndex) => Load(filepath);
        public SramFile(Stream stream, int sramSize, int gameSize, int firstGameOffset, int maxGameIndex) :this(sramSize, gameSize, firstGameOffset, maxGameIndex) => Load(stream);
        public SramFile(int sramSize, int gameSize, int firstGameOffset, int maxGameIndex) => (SramSize, GameSize, FirstGameOffset, MaxGameIndex) = (sramSize, gameSize, firstGameOffset, maxGameIndex);

        public virtual bool IsValid(int gameIndex) => gameIndex >= 0 && gameIndex <= MaxGameIndex;
        public virtual bool IsValid() => true;

        public void Load(string filepath)
        {
            Requires.FileExists(filepath, nameof(filepath));

            Load(new FileStream(filepath, FileMode.Open));
        }
        public void Load(Stream stream) => OnLoad(stream);

        protected virtual void OnLoad(Stream stream) => SramBuffer = LoadSramBufferFromStream(stream);

        private byte[] LoadSramBufferFromStream(Stream stream)
        {
            Requires.NotNull(stream, nameof(stream));

            stream.Seek(0, SeekOrigin.End);

            if (stream.Position != SramSize)
                throw new InvalidSramFileException(FileError.InvalidSize);

            var sram = new byte[SramSize];

            stream.Seek(0, SeekOrigin.Begin);
            stream.Read(sram, 0, SramSize);
            stream.Close();
            
            return sram;
        }

        public byte[] GetGameBytes(int gameIndex)
        {
            Requires.True(IsValid(gameIndex), nameof(gameIndex));

            var startOffset = FirstGameOffset + gameIndex * GameSize;
            var endOffset = startOffset + GameSize;

            return SramBuffer[startOffset..endOffset];
        }

        public void Save(string filepath)
        {
            Requires.NotNullOrEmpty(filepath, nameof(filepath));

            Save(new FileStream(filepath, FileMode.Create));
        }
        public void Save(Stream stream)
        {
            Requires.NotNull(stream, nameof(stream));

            OnSave(stream);
        }

        protected virtual void OnSave(Stream stream)
        {
            stream.Position = 0;
            stream.Write(SramBuffer, 0, SramSize);

            if (stream.Position != SramSize)
                throw new InvalidSramFileException(FileError.InvalidSize);

            stream.Close();
            IsModified = false;
        }
    }
}