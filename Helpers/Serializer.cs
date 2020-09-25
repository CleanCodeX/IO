using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SramCommons.Helpers
{
    public static class Serializer
    {
        public static void Serialize<T>(Stream stream, T value)
            where T : unmanaged
        {
            var tSpan = MemoryMarshal.CreateSpan(ref value, 1);
            var span = MemoryMarshal.AsBytes(tSpan);
            stream.Write(span);
        }

        public static byte[] Serialize<T>(T value)
        {
            var size = Marshal.SizeOf(typeof(T));
            var array = new byte[size];
            var ptr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(value!, ptr, true);
            Marshal.Copy(ptr, array, 0, size);
            Marshal.FreeHGlobal(ptr);
            return array;
        }

        public static T Deserialize<T>(Stream stream)
            where T : unmanaged
        {
            var result = default(T);
            var tSpan = MemoryMarshal.CreateSpan(ref result, 1);
            var span = MemoryMarshal.AsBytes(tSpan);
            stream.Read(span);
            return result;
        }

        public static T Deserialize<T>(ReadOnlyMemory<byte> span)
            where T : struct => Deserialize<T>(span.Span);

        public static T Deserialize<T>(ReadOnlySpan<byte> span)
            where T : struct =>
            MemoryMarshal.Read<T>(span);

        public static T Deserialize<T>(byte[] array)
        {
            var size = Marshal.SizeOf<T>();
            var ptr = Marshal.AllocHGlobal(size);
            Marshal.Copy(array, 0, ptr, size);
            var s = Marshal.PtrToStructure<T>(ptr)!;
            Marshal.FreeHGlobal(ptr);
            return s!;
        }
    }
}