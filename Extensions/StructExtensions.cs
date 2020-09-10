using System;
using System.Runtime.InteropServices;

namespace SramCommons.Extensions
{
    public static class StructExtensions
    {
        /// <summary>
        /// Convert the bytes to a structure in host-endian format (little-endian on PCs).
        /// To use with big-endian data, reverse all of the data bytes and create a struct that is in the reverse order of the data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="buffer">The buffer.</param>
        /// <returns></returns>
        public static T ToStructureHostEndian<T>(this byte[] buffer) where T : struct
        {
            var handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            var stuff = (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T))!;
            handle.Free();
            return stuff;
        }

        /// <summary>
        /// Converts the struct to a byte array in the endianness of this machine.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="structure">The structure.</param>
        /// <returns></returns>
        public static byte[] ToBytesHostEndian<T>(this T structure) 
        {
            var size = Marshal.SizeOf(structure);
            var buffer = new byte[size];
            var handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            Marshal.StructureToPtr(structure!, handle.AddrOfPinnedObject(), true);
            handle.Free();
            return buffer;
        }

        public static T ByteArrayToStructure<T>(this byte[] bytes) where T : struct
            => (T)ByteArrayToStructure(bytes, typeof(T))!;

        public static object? ByteArrayToStructure(this byte[] bytes, Type type)
        {
            var handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            var stuff = Marshal.PtrToStructure(handle.AddrOfPinnedObject(), type);
            handle.Free();
            return stuff;
        }
    }
}
