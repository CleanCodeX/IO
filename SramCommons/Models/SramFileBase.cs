using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using SramCommons.Exceptions;
using SramCommons.Helpers;

namespace SramCommons.Models
{
    public abstract class SramFileBase<TSram, TSramGame> : SramFileBase, ISramFile<TSramGame>
        where TSram : struct
        where TSramGame : struct
    {
        private TSramGame _currentGame;

        protected int FirstGameOffset { get; }
        protected int MaxGameIndex { get; }

        public ref TSramGame CurrentGame => ref _currentGame;
        public TSram Sram { get; }
        public int CurrentGameIndex { get; protected set; }
        
        protected SramFileBase(string filename, int sramSize, int gameSize, int firstGameOffset, int maxGameIndex) :base(sramSize, gameSize)
        {
            Debug.Assert(SramSize == Marshal.SizeOf<TSram>());
            Debug.Assert(gameSize == Marshal.SizeOf<TSramGame>());

            FirstGameOffset = firstGameOffset;
            MaxGameIndex = maxGameIndex;
            SramBuffer = LoadSramBufferFromFile(filename);
            Sram = GetSramStructFromBuffer(SramBuffer);
        }

        public abstract TSramGame GetGame(int gameIndex);

        public bool IsValid() => IsValid(CurrentGameIndex);
        public virtual bool IsValid(int gameIndex) => gameIndex >= 0 && gameIndex <= MaxGameIndex;

        public virtual Span<byte> GetCurrentGameBytes()
        {
            var startOffset = FirstGameOffset + CurrentGameIndex * GameSize;
            var endOffset = startOffset + GameSize;

            return SramBuffer.AsSpan()[startOffset..endOffset];
        }

        private static TSram GetSramStructFromBuffer(byte[] buffer) => Serializer.Deserialize<TSram>(buffer);
    }

    public abstract class SramFileBase
    {
#nullable disable
        public byte[] SramBuffer { get; protected set; }
#nullable restore
        public bool IsModified { get; set; }

        protected int SramSize { get; }
        protected int GameSize { get; }

        protected SramFileBase(int sramSize, int gameSize) => (SramSize, GameSize) = (sramSize, gameSize);

        protected byte[] LoadSramBufferFromFile(string filename)
        {
            using var file = new FileStream(filename, FileMode.Open);

            if (file is null)
                throw new InvalidSramFileException(FileError.FileNotFound);

            file.Seek(0, SeekOrigin.End);

            if (file.Position != SramSize)
                throw new InvalidSramFileException(FileError.InvalidSize);

            var sram = new byte[SramSize];

            file.Seek(0, SeekOrigin.Begin);
            file.Read(sram, 0, SramSize);
            file.Close();

            return sram;
        }

        public virtual bool Save(string filename)
        {
            using var file = new FileStream(filename, FileMode.Create);

            file.Write(SramBuffer, 0, SramSize);

            if (file.Position != SramSize)
                return false;

            file.Close();
            IsModified = false;

            return true;
        }
    }
}