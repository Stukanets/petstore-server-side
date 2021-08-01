using System;
using System.Collections.Generic;
using System.Linq;

namespace Kpi.ServerSide.AutomationFramework.Platform.Enumerable
{
    public static class Extensions
    {
        public static string ToDelimitedString<T>(this IEnumerable<T> source, Func<T, string> func) =>
            ToDelimitedString(source, "?", func);

        public static string ToDelimitedString<T>(this IEnumerable<T> source, string delimiter, Func<T, string> func) =>
            string.Join(delimiter, source.Select(func).ToArray());
    }
}
