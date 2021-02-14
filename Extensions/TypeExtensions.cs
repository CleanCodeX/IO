using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace IO.Extensions
{
	public static class TypeExtensions
	{
		public static IEnumerable<FieldInfo> GetPublicInstanceFields([NotNull] this Type type) => type.GetFields(BindingFlags.Instance | BindingFlags.Public);

		public static IEnumerable<FieldInfo> GetPublicStaicFields([NotNull] this Type type) => type.GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy);

		public static IReadOnlyDictionary<string, T> GetPublicConstants<T>(this Type type) =>
			type
				.GetPublicStaicFields()
				.Where(fi => fi.IsLiteral && !fi.IsInitOnly && fi.FieldType == typeof(T))
				.ToDictionary(k => k.Name, v => (T)v.GetRawConstantValue()!)!;
	}
}
