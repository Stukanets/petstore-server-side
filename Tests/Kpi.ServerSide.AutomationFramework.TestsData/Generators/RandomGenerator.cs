using System;
using System.Text;

namespace Kpi.ServerSide.AutomationFramework.TestsData.Generators
{
    public static class RandomGenerator
    {
        private const string Tag = "QA";

        private static readonly Random GetRandom = new Random();

        public static string NewEmail => $"{Tag}{Guid.NewGuid():N}@gmail.com";

        public static string RandomSsn => GetRandomSsn("1", "-");

        public static string RandomString(int length = 10) => $"{Tag}{RandomString(length, true)}";

        public static DateTime GetRandomDate() => GenerateRandomDates();

        private static string RandomString(int size, bool lowerCase)
        {
            var builder = new StringBuilder();
            var random = new Random();
            for (var i = 0; i < size; i++)
            {
                var ch = Convert.ToChar(Convert.ToInt32(Math.Floor((26 * random.NextDouble()) + 65)));
                builder.Append(ch);
            }

            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }

        private static string GetRandomSsn(string thePrefix, string delimiter = "") =>
            thePrefix + GenerateSsn(delimiter).Substring(thePrefix.Length);

        private static string GenerateSsn(string delimiter)
        {
            var iThree = GetRandomNumber(132, 921);
            var iTwo = GetRandomNumber(12, 83);
            var iFour = GetRandomNumber(1423, 9211);
            return iThree + delimiter + iTwo + delimiter + iFour;
        }

        private static DateTime GenerateRandomDates()
        {
            var rnd = new Random(Guid.NewGuid().GetHashCode());

            var year = rnd.Next(1950, 2000);
            var month = rnd.Next(1, 13);
            var days = rnd.Next(1, DateTime.DaysInMonth(year, month) + 1);

            return new DateTime(year, month, days,
                rnd.Next(0, 24), rnd.Next(0, 60), rnd.Next(0, 60), rnd.Next(0, 1000));
        }

        private static int GetRandomNumber(int min, int max) => GetRandom.Next(min, max);
    }
}
