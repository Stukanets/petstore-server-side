using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace Kpi.ServerSide.AutomationFramework.Platform.Enum
{
    public static class EnumExtensions
    {
        public static T ToEnum<T>(this string value, bool ignoreCase = true) =>
            (T)System.Enum.Parse(typeof(T), value, ignoreCase);

        public static string GetEnumMemberValue<T>(T value)
            where T : struct, IConvertible =>
            typeof(T)
                .GetTypeInfo()
                .DeclaredMembers
                .SingleOrDefault(x => x.Name == value.ToString(CultureInfo.InvariantCulture))
                ?.GetCustomAttribute<EnumMemberAttribute>(false)
                ?.Value;
    }
}
