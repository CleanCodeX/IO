using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Common.Shared.Min.Extensions;
using IO.Properties;

namespace IO.Extensions
{
	public static class EnumExtensions
	{
		public static string ToFlagsString([NotNull] this Enum source) => source.ToString() == "0" ? Resources.EnumNone : source.ToString();

		public static IDictionary<string, Enum> ToDictionary<TEnum>(this TEnum source) where TEnum: struct, Enum => Enum.GetNames<TEnum>().ToDictionary(k => k, v => (Enum)v.ParseEnum<TEnum>());

		#region UInt16

		public static Enum InvertUInt16Flags([NotNull] this Enum source, Enum flags)
		{
			var intFlag = (UInt16)(object)flags;
			var intFlags = (UInt16)(object)source;

			if ((intFlags & intFlag) == intFlag)
				intFlags &= (UInt16)~intFlag;
			else
				intFlags |= intFlag;

			return (Enum)Enum.ToObject(source.GetType(), intFlags);
		}

		public static Enum SetUInt16Flags([NotNull] this Enum source, Enum flags)
		{
			var intFlags = (UInt16)(object)source;
			intFlags |= (UInt16)(object)flags;

			return (Enum)Enum.ToObject(source.GetType(), intFlags);
		}

		public static Enum UnsetUInt16Flags([NotNull] this Enum source, Enum flags)
		{
			var intFlags = (UInt16)(object)source;
			intFlags &= (UInt16)~(UInt16)(object)flags;

			return (Enum)Enum.ToObject(source.GetType(), intFlags);
		}

		public static bool HasUInt16Flag([NotNull] this Enum source, Enum flag) => ((UInt16)(object)source & (UInt16)(object)flag) != 0;
		public static bool HasUInt16Flags([NotNull] this Enum source, Enum flags)
		{
			var intFlags = (UInt16)(object)source;
			var intFlag = (UInt16)(object)flags;

			return (intFlags & intFlag) == intFlag;
		}

		#endregion

		#region UInt32

		public static Enum InvertUInt32Flags([NotNull] this Enum source, Enum flags)
		{
			var intFlag = (UInt32)(object)flags;
			var intFlags = (UInt32)(object)source;

			if ((intFlags & intFlag) == intFlag)
				intFlags &= ~intFlag;
			else
				intFlags |= intFlag;

			return (Enum)Enum.ToObject(source.GetType(), intFlags);
		}

		public static Enum SetUInt32Flags([NotNull] this Enum source, Enum flags)
		{
			var intFlags = (UInt32)(object)source;
			intFlags |= (UInt32)(object)flags;

			return (Enum)Enum.ToObject(source.GetType(), intFlags);
		}

		public static Enum UnsetUInt32Flags([NotNull] this Enum source, Enum flags)
		{
			var intFlags = (UInt32)(object)source;
			intFlags &= ~(UInt32)(object)flags;

			return (Enum)Enum.ToObject(source.GetType(), intFlags);
		}

		public static bool HasUInt32Flag([NotNull] this Enum source, Enum flag) => ((UInt32)(object)source & (UInt32)(object)flag) != 0;
		public static bool HasUInt32Flags([NotNull] this Enum source, Enum flags)
		{
			var intFlags = (UInt32)(object)source;
			var intFlag = (UInt32)(object)flags;

			return (intFlags & intFlag) == intFlag;
		}

		#endregion

		#region UInt64

		public static Enum InvertUInt64Flags([NotNull] this Enum source, Enum flags)
		{
			var intFlag = (UInt64)(object)flags;
			var intFlags = (UInt64)(object)source;

			if ((intFlags & intFlag) == intFlag)
				intFlags &= ~intFlag;
			else
				intFlags |= intFlag;

			return (Enum)Enum.ToObject(source.GetType(), intFlags);
		}

		public static Enum SetUInt64Flags([NotNull] this Enum source, Enum flags)
		{
			var intFlags = (UInt64)(object)source;
			intFlags |= (UInt64)(object)flags;

			return (Enum)Enum.ToObject(source.GetType(), intFlags);
		}

		public static Enum UnsetUInt64Flags([NotNull] this Enum source, Enum flags)
		{
			var intFlags = (UInt64)(object)source;
			intFlags &= ~(UInt64)(object)flags;

			return (Enum)Enum.ToObject(source.GetType(), intFlags);
		}

		public static bool HasUInt64Flag([NotNull] this Enum source, Enum flag) => ((UInt64)(object)source & (UInt64)(object)flag) != 0;
		public static bool HasUInt64Flags([NotNull] this Enum source, Enum flags)
		{
			var intFlags = (UInt64)(object)source;
			var intFlag = (UInt64)(object)flags;

			return (intFlags & intFlag) == intFlag;
		}

		#endregion

		#region Generic Enum

		#region UInt16

		public static TEnum InvertUInt16Flags<TEnum>(this TEnum source, TEnum flags)
		where TEnum : struct, Enum
		{
			var intFlag = (UInt16)(object)flags;
			var intFlags = (UInt16)(object)source;

			if ((intFlags & intFlag) == intFlag)
				intFlags &= (UInt16)~intFlag;
			else
				intFlags |= intFlag;

			return (TEnum)Enum.ToObject(source.GetType(), intFlags);
		}

		public static TEnum SetUInt16Flags<TEnum>(this TEnum source, TEnum flags)
		where TEnum : struct, Enum
		{
			var intFlags = (UInt16)(object)source;
			intFlags |= (UInt16)(object)flags;

			return (TEnum)Enum.ToObject(source.GetType(), intFlags);
		}

		public static TEnum UnsetUInt16Flags<TEnum>(this TEnum source, TEnum flags)
		where TEnum : struct, Enum
		{
			var intFlags = (UInt16)(object)source;
			intFlags &= (UInt16)~(UInt16)(object)flags;

			return (TEnum)Enum.ToObject(source.GetType(), intFlags);
		}

		public static bool HasUInt16Flag<TEnum>(this TEnum source, TEnum flag) => ((UInt16)(object)source & (UInt16)(object)flag) != 0;
		public static bool HasUInt16Flags<TEnum>(this TEnum source, TEnum flags)
		where TEnum : struct, Enum
		{
			var intFlags = (UInt16)(object)source;
			var intFlag = (UInt16)(object)flags;

			return (intFlags & intFlag) == intFlag;
		}

		#endregion

		#region UInt32

		public static TEnum InvertUInt32Flags<TEnum>(this TEnum source, TEnum flags)
		where TEnum : struct, Enum
		{
			var intFlag = (UInt32)(object)flags;
			var intFlags = (UInt32)(object)source;

			if ((intFlags & intFlag) == intFlag)
				intFlags &= ~intFlag;
			else
				intFlags |= intFlag;

			return (TEnum)Enum.ToObject(source.GetType(), intFlags);
		}

		public static TEnum SetUInt32Flags<TEnum>(this TEnum source, TEnum flags)
		where TEnum : struct, Enum
		{
			var intFlags = (UInt32)(object)source;
			intFlags |= (UInt32)(object)flags;

			return (TEnum)Enum.ToObject(source.GetType(), intFlags);
		}

		public static TEnum UnsetUInt32Flags<TEnum>(this TEnum source, TEnum flags)
		where TEnum : struct, Enum
		{
			var intFlags = (UInt32)(object)source;
			intFlags &= ~(UInt32)(object)flags;

			return (TEnum)Enum.ToObject(source.GetType(), intFlags);
		}

		public static bool HasUInt32Flag<TEnum>(this TEnum source, TEnum flag) => ((UInt32)(object)source & (UInt32)(object)flag) != 0;
		public static bool HasUInt32Flags<TEnum>(this TEnum source, TEnum flags)
		where TEnum : struct, Enum
		{
			var intFlags = (UInt32)(object)source;
			var intFlag = (UInt32)(object)flags;

			return (intFlags & intFlag) == intFlag;
		}

		#endregion

		#region UInt64

		public static TEnum InvertUInt64Flags<TEnum>(this TEnum source, TEnum flags)
		where TEnum : struct, Enum
		{
			var intFlag = (UInt64)(object)flags;
			var intFlags = (UInt64)(object)source;

			if ((intFlags & intFlag) == intFlag)
				intFlags &= ~intFlag;
			else
				intFlags |= intFlag;

			return (TEnum)Enum.ToObject(source.GetType(), intFlags);
		}

		public static TEnum SetUInt64Flags<TEnum>(this TEnum source, TEnum flags)
		where TEnum : struct, Enum
		{
			var intFlags = (UInt64)(object)source;
			intFlags |= (UInt64)(object)flags;

			return (TEnum)Enum.ToObject(source.GetType(), intFlags);
		}

		public static TEnum UnsetUInt64Flags<TEnum>(this TEnum source, TEnum flags)
		where TEnum : struct, Enum
		{
			var intFlags = (UInt64)(object)source;
			intFlags &= ~(UInt64)(object)flags;

			return (TEnum)Enum.ToObject(source.GetType(), intFlags);
		}

		public static bool HasUInt64Flag<TEnum>(this TEnum source, TEnum flag) => ((UInt64)(object)source & (UInt64)(object)flag) != 0;
		public static bool HasUInt64Flags<TEnum>(this TEnum source, TEnum flags)
		where TEnum : struct, Enum
		{
			var intFlags = (UInt64)(object)source;
			var intFlag = (UInt64)(object)flags;

			return (intFlags & intFlag) == intFlag;
		}

		#endregion

		#endregion
	}
}