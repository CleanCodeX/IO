﻿using System;
using System.Runtime.InteropServices;

namespace SramCommons.Extensions
{
	/// <summary>
	/// Memory extensions for framework versions that do not support the new Memory overloads on various framework methods.
	/// </summary>
	internal static class MemoryExtensions
	{
		public static ArraySegment<byte> GetUnderlyingArray(this Memory<byte> bytes) => GetUnderlyingArray((ReadOnlyMemory<byte>)bytes);
		public static ArraySegment<byte> GetUnderlyingArray(this ReadOnlyMemory<byte> bytes)
		{
			if (!MemoryMarshal.TryGetArray(bytes, out var arraySegment)) throw new NotSupportedException("This Memory does not support exposing the underlying array.");
			return arraySegment;
		}
	}
}
