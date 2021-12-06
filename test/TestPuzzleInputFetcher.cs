using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;

namespace AdventOfCode2021
{
    [TestClass]
    public class TestPuzzleInputFetcher
    {
        [TestMethod]
        public void TestFetchPuzzleInputFromLocalStore()
        {
            var mock = new Mock<PuzzleInputFetcher>
            {
                CallBase = true
            };
            for (var day = 1; day <= 25; day++)
            {
                var input = RandomPuzzle();
                mock.Setup(fetcher => fetcher.FetchLocalPuzzleInput(day)).Returns(input);
                mock.Setup(fetcher => fetcher.FetchRemotePuzzleInput(day)).Throws(new Exception("Expected"));
                mock.Setup(fetcher => fetcher.StorePuzzleInputLocally(day, input)).Throws(new Exception("Expected"));
                var fetcher = mock.Object;
                Assert.AreEqual(input, fetcher.FetchPuzzleInput(day));
                Assert.AreEqual(input, fetcher.FetchPuzzleInput(day));
                mock.Verify(fetcher => fetcher.FetchLocalPuzzleInput(day), Times.Once());
                mock.Verify(fetcher => fetcher.FetchRemotePuzzleInput(day), Times.Never());
                mock.Verify(fetcher => fetcher.StorePuzzleInputLocally(day, input), Times.Never());
            }
        }

        // TODO: Fill out the rest of the tests and use WireMock to mock the HTTP client

        private static string RandomPuzzle()
        {
            // Puzzle inputs tend to contain a wide variety of ASCII characters including line feed.
            // They can also be fairly large.
            var charset = "\n"                 // ASCII code 10 (line feed)
                + " !\"#$%&'()*+,-./"          // ASCII codes 32-47 (symbols)
                + "0123456789"                 // ASCII codes 48-57 (digits)
                + ":;<=>?@"                    // ASCII codes 58-64 (symbols)
                + "ABCDEFGHIJKLMNOPQRSTUVWXYZ" // ASCII codes 65-90 (uppercase letters)
                + "[\\]^_`"                    // ASCII codes 91-96 (symbols)
                + "abcdefghijklmnopqrstuvwxyz" // ASCII codes 97-122 (lowercase letters)
                + "{|}~";                      // ASCII codes 123-126 (symbols)
            return RandomString(charset, 65535);
        }

        private static string RandomSessionToken()
        {
            // Session tokens appear to be 96 characters of ASCII hex digits
            return RandomString("0123456789abcdef", 96);
        }

        private static readonly Random random = new();

        private static string RandomString(string charset, int size)
        {
            return new string(Enumerable.Repeat(charset, size).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
