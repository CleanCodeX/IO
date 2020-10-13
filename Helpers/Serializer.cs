using System;
using System.IO;
using System.Runtime.InteropServices;

namespace SramCommons.Helpers
{
	/// <summary>
	/// Serializes and deserializes strcutures
	/// </summary>
	public static class Serializer
	{
		/// <summary>Serializes data into a stream</summary>
		public static void Serialize<T>(Stream stream, T data)
			where T : unmanaged
		{
			var tSpan = MemoryMarshal.CreateSpan(ref data, 1);
			var span = MemoryMarshal.AsBytes(tSpan);
			stream.Write(span);
		}

		/// <summary>Serializes data into an array</summary>
		public static byte[] Serialize<T>(T data)
		{
			var size = Marshal.SizeOf(typeof(T));
			var array = new byte[size];
			var ptr = Marshal.AllocHGlobal(size);
			Marshal.StructureToPtr(data!, ptr, true);
			Marshal.Copy(ptr, array, 0, size);
			Marshal.FreeHGlobal(ptr);
			return array;
		}

		/// <summary>Deserializes a data type from stream</summary>
		public static T Deserialize<T>(Stream stream)
			where T : unmanaged
		{
			var result = default(T);
			var tSpan = MemoryMarshal.CreateSpan(ref result, 1);
			var span = MemoryMarshal.AsBytes(tSpan);
			stream.Read(span);
			return result;
		}

		/// <summary>Deserializes a data type from memory</summary>
		public static T Deserialize<T>(ReadOnlyMemory<byte> memory)
			where T : struct => Deserialize<T>(memory.Span);

		/// <summary>Deserializes a data type from span</summary>
		public static T Deserialize<T>(ReadOnlySpan<byte> span)
			where T : struct =>
			MemoryMarshal.Read<T>(span);

		/// <summary>Deserializes a data type from yte array</summary>
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