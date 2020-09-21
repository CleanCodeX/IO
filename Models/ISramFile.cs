using System;

namespace SramCommons.Models
{
    public interface ISramFile<out TSramGame> : ISramFile
        where TSramGame : struct
    {
        TSramGame GetGame(int gameIndex);
        
        public bool IsValid(int gameIndex);
    }

    public interface ISramFile
    {
        bool Save(string filepath);
        public bool IsValid();
        Span<byte> GetCurrentGameBytes();
    }
}