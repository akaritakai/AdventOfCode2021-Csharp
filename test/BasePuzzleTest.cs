using System;
using System.IO;
using System.Reflection;

namespace AdventOfCode2021
{
    internal class BasePuzzleTest
    {
        public static string PuzzleInput(int day)
        {
            var name = "AdventOfCode2021.resources." + day;
            var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(name);
            using var reader = new StreamReader(stream!);
            return reader.ReadToEnd();
        }
    }
}
