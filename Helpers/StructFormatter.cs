using System;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using Common.Shared.Min.Extensions;
using IO.Extensions;

namespace IO.Helpers
{
	public static class StructFormatter
	{
		/// <summary>
		/// Formates the structure recursively by an optionally specified delimiter.
		/// </summary>
		/// <param name="source"></param>
		/// <param name="initialIdentSize">The initial identation</param>
		/// <param name="delimiter">The delimiter for separation</param>
		/// <returns></returns>
		public static string FormatAsString<T>(T source, int initialIdentSize = 1, string? delimiter = null)
			where T : struct => InternalFormatAsString(source, initialIdentSize, delimiter);

		private static string InternalFormatAsString(this object source, int identSize, string? delimiter = null)
		{
			var ident = " ".Repeat(identSize);
			var fieldInfos = source.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
			var newLine = Environment.NewLine;
			const int maxArrayStringSize = 100;
			const int identIncrease = 2;

			StringBuilder sb = new(fieldInfos.Length);
			foreach (var fieldInfo in fieldInfos)
			{
				var fieldType = fieldInfo.FieldType;
				var value = fieldInfo.GetValue(source)!;
				var fieldName = fieldInfo.Name;
				//Debug.Assert(fieldName != "AlchemyMinorLevels");

				if (fieldType.IsArray)
				{
					var elementType = fieldType.GetElementType()!;
					if (Type.GetTypeCode(elementType) == TypeCode.Object)
					{
						var i = 0;
						foreach (var element in (Array)value)
						{
							var complexValue = InternalFormatAsString(element, identSize + identIncrease, delimiter);
							sb.Append(newLine + $"{ident} ¬ {fieldName}[{i}]{complexValue}");
							++i;
						}
					}
					else
					{
						sb.Append(newLine + $"{ident} | {fieldName}: ");
						var length = ((Array) value).Length;
						sb.Append(elementType.IsPrimitive
							? $"{elementType.Name}[{length}] {((Array)value).FormatAsString().TruncateAt(maxArrayStringSize)!.Replace("…", "...")}"
							: $"{elementType.Name}[{length}]");

					}
				}
				else if (Type.GetTypeCode(fieldType) == TypeCode.Object)
				{
					sb.Append(newLine);
					
					var isComplexMember = fieldType.IsDefined<HasComplexMembersAttribute>();
					var hasToStringOverride = fieldType.IsDefined<HasToStringOverrideAttribute>();
					var memberFieldCount = fieldType.GetFields().Length;

					if (!hasToStringOverride && (isComplexMember || memberFieldCount > 1))
					{
						var complexValue = InternalFormatAsString(value, identSize + identIncrease, delimiter);
						sb.Append($"{ident} ¤ {fieldName} {complexValue}");
					}
					else
						sb.Append($"{ident} | {fieldName}: {value}");
				}
				else
					sb.Append(newLine + $"{ident} | {fieldName}: {value}");
			}

			return delimiter is null ? sb.ToString() : sb.ToString().Replace(newLine, delimiter);
		}
	}
}
