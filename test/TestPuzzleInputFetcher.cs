using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.IO;
using System.Linq;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace AdventOfCode2021
{
    [TestClass]
    public class TestPuzzleInputFetcher
    {
        [TestMethod]
        public void TestFetchPuzzleInputFromLocalStore()
        {
            using var localPuzzleStore = new LocalPuzzleStore();
            using var localSessionToken = new LocalSessionToken();
            using var server = WireMockServer.Start();
            var mock = new Mock<PuzzleInputFetcher>(localPuzzleStore.Location(), localSessionToken.Location(), server.Urls[0])
            {
                CallBase = true
            };
            for (var day = 1; day <= 25; day++)
            {
                var input = RandomPuzzle();
                File.WriteAllText(localPuzzleStore.PuzzleLocation(day), input);
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

        [TestMethod]
        public void TestFetchPuzzleInputFromRemoteStore()
        {
            using var localPuzzleStore = new LocalPuzzleStore();
            using var localSessionToken = new LocalSessionToken();
            using var server = WireMockServer.Start();
            var mock = new Mock<PuzzleInputFetcher>(localPuzzleStore.Location(), localSessionToken.Location(), server.Urls[0])
            {
                CallBase = true
            };
            for (var day = 1; day <= 25; day++)
            {
                var input = RandomPuzzle();
                mock.Setup(fetcher => fetcher.FetchLocalPuzzleInput(day)).Throws(new Exception("Expected"));
                server.Given(Request.Create()
                                    .WithPath("/2021/day/" + day + "/input")
                                    .WithHeader("Cookie", "session=" + localSessionToken.SessionToken())
                                    .UsingGet())
                      .RespondWith(Response.Create()
                                           .WithStatusCode(200)
                                           .WithBody(input));
                var fetcher = mock.Object;
                Assert.AreEqual(input, fetcher.FetchPuzzleInput(day));
                Assert.AreEqual(input, fetcher.FetchPuzzleInput(day));
                Assert.AreEqual(input, File.ReadAllText(fetcher.PuzzleStorePath(day)));
                mock.Verify(fetcher => fetcher.FetchLocalPuzzleInput(day), Times.Once());
                mock.Verify(fetcher => fetcher.FetchRemotePuzzleInput(day), Times.Once());
                mock.Verify(fetcher => fetcher.StorePuzzleInputLocally(day, input), Times.Once());
            }
        }

        [TestMethod]
        public void TestFetchPuzleInputThrowsIfPuzzleRequestedEarly()
        {
            using var localPuzzleStore = new LocalPuzzleStore();
            using var localSessionToken = new LocalSessionToken();
            using var server = WireMockServer.Start();
            var mock = new Mock<PuzzleInputFetcher>(localPuzzleStore.Location(), localSessionToken.Location(), server.Urls[0])
            {
                CallBase = true
            };
            for (var day = 1; day <= 25; day++)
            {
                var input = RandomPuzzle();
                mock.Setup(fetcher => fetcher.FetchLocalPuzzleInput(day)).Throws(new Exception("Expected"));
                server.Given(Request.Create()
                                    .WithPath("/2021/day/" + day + "/input")
                                    .WithHeader("Cookie", "session=" + localSessionToken.SessionToken())
                                    .UsingGet())
                      .RespondWith(Response.Create()
                                           .WithStatusCode(404)
                                           .WithBody(@"Please don't repeatedly request this endpoint before it unlocks! 
                                                      The calendar countdown is synchronized with the server time; 
                                                      the link will be enabled on the calendar the instant this puzzle becomes available."));
                var fetcher = mock.Object;
                Assert.ThrowsException<Exception>(() => fetcher.FetchPuzzleInput(day));
                mock.Verify(fetcher => fetcher.FetchLocalPuzzleInput(day), Times.Once());
                mock.Verify(fetcher => fetcher.FetchRemotePuzzleInput(day), Times.Once());
                mock.Verify(fetcher => fetcher.StorePuzzleInputLocally(day, input), Times.Never());
            }
        }

        [TestMethod]
        public void TestRemotePuzzleInputUrl()
        {
            using var localPuzzleStore = new LocalPuzzleStore();
            using var localSessionToken = new LocalSessionToken();
            var fetcher = new PuzzleInputFetcher(localPuzzleStore.Location(), localSessionToken.Location(), "https://adventofcode.com");
            for (var day = 1; day <= 25; day++)
            {
                Assert.AreEqual("https://adventofcode.com/2021/day/" + day + "/input", fetcher.RemotePuzzleInputUrl(day));
            }
        }

        [TestMethod]
        public void TestFetchPuzzleInputFromRemoteStoreFailsIfMissingSessionToken()
        {
            using var localPuzzleStore = new LocalPuzzleStore();
            using var server = WireMockServer.Start();
            var nonExistentPath = Path.GetTempPath() + Guid.NewGuid().ToString();
            var mock = new Mock<PuzzleInputFetcher>(localPuzzleStore.Location(), nonExistentPath, server.Urls[0])
            {
                CallBase = true
            };
            for (var day = 1; day <= 25; day++)
            {
                var input = RandomPuzzle();
                mock.Setup(fetcher => fetcher.FetchLocalPuzzleInput(day)).Throws(new Exception("Expected"));
                server.Given(Request.Create()
                                    .WithPath("/2021/day/" + day + "/input")
                                    .UsingGet())
                      .RespondWith(Response.Create()
                                           .WithStatusCode(200)
                                           .WithBody(input));
                var fetcher = mock.Object;
                Assert.ThrowsException<Exception>(() => fetcher.FetchPuzzleInput(day));
                mock.Verify(fetcher => fetcher.FetchLocalPuzzleInput(day), Times.Once());
                mock.Verify(fetcher => fetcher.FetchRemotePuzzleInput(day), Times.Once());
                mock.Verify(fetcher => fetcher.StorePuzzleInputLocally(day, input), Times.Never());
            }
        }

        [TestMethod]
        public void TestFetchPuzzleInputThrowsWhenAllSourcesUnavailable()
        {
            using var localPuzzleStore = new LocalPuzzleStore();
            using var localSessionToken = new LocalSessionToken();
            using var server = WireMockServer.Start();
            var mock = new Mock<PuzzleInputFetcher>(localPuzzleStore.Location(), localSessionToken.Location(), server.Urls[0])
            {
                CallBase = true
            };
            for (var day = 1; day <= 25; day++)
            {
                var input = RandomPuzzle();
                mock.Setup(fetcher => fetcher.FetchLocalPuzzleInput(day)).Throws(new Exception("Expected"));
                mock.Setup(fetcher => fetcher.FetchRemotePuzzleInput(day)).Throws(new Exception("Expected"));
                mock.Setup(fetcher => fetcher.StorePuzzleInputLocally(day, input)).Verifiable();
                var fetcher = mock.Object;
                Assert.ThrowsException<Exception>(() => fetcher.FetchPuzzleInput(day));
                mock.Verify(fetcher => fetcher.FetchLocalPuzzleInput(day), Times.Once());
                mock.Verify(fetcher => fetcher.FetchRemotePuzzleInput(day), Times.Once());
                mock.Verify(fetcher => fetcher.StorePuzzleInputLocally(day, input), Times.Never());
            }
        }

        [TestMethod]
        public void TestFetchRemotePuzzleIssueWhenStoringPuzzleDoesNotThrow()
        {
            using var localPuzzleStore = new LocalPuzzleStore();
            using var localSessionToken = new LocalSessionToken();
            using var server = WireMockServer.Start();
            var mock = new Mock<PuzzleInputFetcher>(localPuzzleStore.Location(), localSessionToken.Location(), server.Urls[0])
            {
                CallBase = true
            };
            for (var day = 1; day <= 25; day++)
            {
                var input = RandomPuzzle();
                mock.Setup(fetcher => fetcher.FetchLocalPuzzleInput(day)).Throws(new Exception("Expected"));
                mock.Setup(fetcher => fetcher.StorePuzzleInputLocally(day, input)).Throws(new Exception("Expected"));
                server.Given(Request.Create()
                                    .WithPath("/2021/day/" + day + "/input")
                                    .WithHeader("Cookie", "session=" + localSessionToken.SessionToken())
                                    .UsingGet())
                      .RespondWith(Response.Create()
                                           .WithStatusCode(200)
                                           .WithBody(input));
                var fetcher = mock.Object;
                Assert.AreEqual(input, fetcher.FetchPuzzleInput(day));
                Assert.AreEqual(input, fetcher.FetchPuzzleInput(day));
                mock.Verify(fetcher => fetcher.FetchLocalPuzzleInput(day), Times.Once());
                mock.Verify(fetcher => fetcher.FetchRemotePuzzleInput(day), Times.Once());
                mock.Verify(fetcher => fetcher.StorePuzzleInputLocally(day, input), Times.Once());
            }
        }

        [TestMethod]
        public void TestFetchPuzzleInputThrowsIfSessionTokenWrong()
        {
            using var localPuzzleStore = new LocalPuzzleStore();
            using var server = WireMockServer.Start();
            var nonExistentPath = Path.GetTempPath() + Guid.NewGuid().ToString();
            var mock = new Mock<PuzzleInputFetcher>(localPuzzleStore.Location(), nonExistentPath, server.Urls[0])
            {
                CallBase = true
            };
            for (var day = 1; day <= 25; day++)
            {
                var input = RandomPuzzle();
                mock.Setup(fetcher => fetcher.FetchLocalPuzzleInput(day)).Throws(new Exception("Expected"));
                server.Given(Request.Create()
                                    .WithPath("/2021/day/" + day + "/input")
                                    .UsingGet())
                      .RespondWith(Response.Create()
                                           .WithStatusCode(400)
                                           .WithBody("Puzzle inputs differ by user. Please log in to get your puzzle input."));
                var fetcher = mock.Object;
                Assert.ThrowsException<Exception>(() => fetcher.FetchPuzzleInput(day));
                mock.Verify(fetcher => fetcher.FetchLocalPuzzleInput(day), Times.Once());
                mock.Verify(fetcher => fetcher.FetchRemotePuzzleInput(day), Times.Once());
                mock.Verify(fetcher => fetcher.StorePuzzleInputLocally(day, input), Times.Never());
            }
        }

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
            return RandomString(charset, 10); // TODO: Change this to 65536 later
        }

        private static string RandomSessionToken()
        {
            // Session tokens appear to be 96 characters of ASCII hex digits
            return RandomString("0123456789abcdef", 96);
        }

        private static readonly Random Random = new();

        private static string RandomString(string charset, int size)
        {
            return new string(Enumerable.Repeat(charset, size).Select(s => s[Random.Next(s.Length)]).ToArray());
        }

        private class LocalPuzzleStore : IDisposable
        {
            private readonly string _location;

            public LocalPuzzleStore()
            {
                _location = Path.GetTempPath() + Guid.NewGuid();
                Directory.CreateDirectory(_location);
            }

            public string Location() { 
                return _location;
            }

            public string PuzzleLocation(int day)
            {
                return Path.Join(_location, day.ToString());
            }

            public void Dispose()
            {
                Directory.Delete(_location, true);
            }
        }

        private class LocalSessionToken : IDisposable
        {
            private readonly string _sessionToken;
            private readonly string _location;

            public LocalSessionToken()
            {
                _location = Path.GetTempPath() + Guid.NewGuid();
                Directory.CreateDirectory(_location);
                var file = Path.Join(_location, "cookie.txt");
                _sessionToken = RandomSessionToken();
                File.WriteAllText(file, _sessionToken);
            }

            public string Location()
            {
                return Path.Join(_location, "cookie.txt");
            }

            public string SessionToken()
            {
                return _sessionToken;
            }

            public void Dispose()
            {
                Directory.Delete(_location, true);
            }
        }
    }
}
