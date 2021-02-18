using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace IO.Extensions
{
	public static class TypeExtensions
	{
		public static IEnumerable<FieldInfo> GetPublicInstanceStructFields([NotNull] this Type type) => type.GetFields(BindingFlags.Instance | BindingFlags.Public).Where(e => e.FieldType.IsStruct());

		public static bool IsStruct([NotNull] this Type source) => source.IsValueType && !source.IsPrimitive && !source.IsEnum;

		public static IEnumerable<FieldInfo> GetPublicInstanceFields([NotNull] this Type type) => type.GetFields(BindingFlags.Instance | BindingFlags.Public);

		public static IEnumerable<FieldInfo> GetPublicStaticFields([NotNull] this Type type) => type.GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy);

		public static IReadOnlyDictionary<string, T> GetPublicConstants<T>([NotNull] this Type type) =>
			type
				.GetPublicStaticFields()
				.Where(fi => fi.IsLiteral && !fi.IsInitOnly && fi.FieldType == typeof(T))
				.ToDictionary(k => k.Name, v => (T)v.GetRawConstantValue()!)!;

		public static bool HasOverride([NotNull] this Type type, [NotNull] string name, [NotNull] params Type[] argTypes)
		{
			const BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly;
			var method = type.GetMethod(name, bindingFlags, null, CallingConventions.HasThis, argTypes, Array.Empty<ParameterModifier>());

			return method is not null;
		}
	}
}
