using System;
using System.Collections.Concurrent;
using System.IO;
using System.Net.Http;

namespace AdventOfCode2021
{
    public class PuzzleInputFetcher
    {
        private readonly ConcurrentDictionary<int, string> _cache = new();
        private readonly string _puzzleStorePath;
        private readonly string _sessionTokenPath;
        private readonly string _baseUrl;
        private readonly object _sessionTokenLock = new();
        private string? _sessionToken;
        
        public PuzzleInputFetcher()
        {
            _puzzleStorePath = "puzzle";
            _sessionTokenPath = "cookie.txt";
            _baseUrl = "https://adventofcode.com";
        }

        public PuzzleInputFetcher(string puzzleStorePath, string sessionTokenPath, string baseUrl)
        {
            _puzzleStorePath = puzzleStorePath;
            _sessionTokenPath = sessionTokenPath;
            _baseUrl = baseUrl;
        }

        public string FetchPuzzleInput(int day)
        {
            return _cache.GetOrAdd(day, _ =>
            {
                try
                {
                    try  
                    {
                        return FetchLocalPuzzleInput(day);
                    }
                    catch (Exception)
                    {
                        // ignored
                    }

                    var input = FetchRemotePuzzleInput(day);

                    try
                    {
                        StorePuzzleInputLocally(day, input);
                    }
                    catch (Exception)
                    {
                        // ignored
                    }

                    return input;
                } catch (Exception e)
                {
                    throw new Exception("Couldn't get puzzle input for day " + day, e);
                }
            });
        }

        public virtual string FetchLocalPuzzleInput(int day)
        {
            return File.ReadAllText(PuzzleStorePath(day));
        }

        public virtual void StorePuzzleInputLocally(int day, string input)
        {
            Directory.CreateDirectory(_puzzleStorePath);
            File.WriteAllText(PuzzleStorePath(day), input);
        }

        public virtual string FetchRemotePuzzleInput(int day)
        {
            using var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, RemotePuzzleInputUrl(day));
            request.Headers.Add("Cookie", "session=" + SessionToken());
            var response = client.SendAsync(request).Result;
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Request was not successful. Status code = " + response.StatusCode);
            }
            return response.Content.ReadAsStringAsync().Result;
        }

        public string PuzzleStorePath(int day)
        {
            return Path.Join(_puzzleStorePath, day.ToString());
        }

        public string RemotePuzzleInputUrl(int day)
        {
            return _baseUrl + "/2021/day/" + day + "/input";
        }

        private string SessionToken()
        {
            lock (_sessionTokenLock)
            {
                try
                {
                    return _sessionToken ??= File.ReadAllText(_sessionTokenPath).Trim();
                } catch (Exception e)
                {
                    throw new Exception("Couldn't get session data from " + _sessionTokenPath, e);
                }
            }
        }
    }
}
