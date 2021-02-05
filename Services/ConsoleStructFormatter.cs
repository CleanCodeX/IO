using System;
using System.Reflection;
using System.Text;
using Common.Shared.Min.Extensions;
using IO.Extensions;
using IO.Helpers;

namespace IO.Services
{
	/// <inheritdoc/>
	public class ConsoleStructFormatter: IStructFormatter
	{
		private static readonly StructFormattingOptions DefaultOptions = new();
	
		public string Format<T>(T source, StructFormattingOptions? options = default)
			where T : struct => InternalFormat(source, 0, options);

		private static string InternalFormat(in object source, int currentIdentSize, StructFormattingOptions? options = default)
		{
			options ??= DefaultOptions;
			var identSize = options.IdentSize;

			currentIdentSize += identSize;

			var newLine = options.NewLine;
			var memberPrefix = options.MemberPrefix;
			var complexMemberPrefix = options.ComplexMemberPrefix;
			var ident = " ".Repeat(identSize);

			var fieldInfos = source.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
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
							var complexValue = InternalFormat(element, currentIdentSize, options);
							sb.Append(newLine + $"{ident} ¬ {fieldName}[{i}]{complexValue}");
							++i;
						}
					}
					else
					{
						sb.Append(newLine + $"{ident} {memberPrefix} {fieldName}: ");
						var length = ((Array) value).Length;
						sb.Append(elementType.IsPrimitive
							? $"{elementType.Name}[{length}] {((Array)value).Format().TruncateAt(options.MaxArrayStringLength)!.Replace("…", options.Elipsis)}"
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
						var complexValue = InternalFormat(value, currentIdentSize, options);
						sb.Append($"{ident} {complexMemberPrefix} {fieldName} {complexValue}");
					}
					else
						sb.Append($"{ident} {memberPrefix} {fieldName}: {value}");
				}
				else
					sb.Append(newLine + $"{ident} {memberPrefix} {fieldName}: {value}");
			}

			return sb.ToString();
		}
	}
}
