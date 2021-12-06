using System.Collections.Concurrent;

namespace AdventOfCode2021
{
    public class PuzzleInputFetcher
    {
        private readonly ConcurrentDictionary<int, string> cache = new();
        private readonly string puzzleStorePath;
        private readonly string sessionTokenPath;
        private readonly object sessionTokenLock = new();
        private string? sessionToken;
        
        public PuzzleInputFetcher()
        {
            puzzleStorePath = "puzzle";
            sessionTokenPath = "cookie.txt";
        }

        public PuzzleInputFetcher(string puzzleStorePath, string sessionTokenPath)
        {
            this.puzzleStorePath = puzzleStorePath;
            this.sessionTokenPath = sessionTokenPath;
        }

        public string FetchPuzzleInput(int day)
        {
            return cache.GetOrAdd(day, key =>
            {
                try
                {
                    try  
                    {
                        return FetchLocalPuzzleInput(day);
                    }
                    catch (Exception)
                    {
                    }

                    var input = FetchRemotePuzzleInput(day);

                    try
                    {
                        StorePuzzleInputLocally(day, input);
                    }
                    catch (Exception)
                    {
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
            Directory.CreateDirectory(puzzleStorePath);
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
            return Path.Join(puzzleStorePath, day.ToString());
        }

        public virtual string RemotePuzzleInputUrl(int day)
        {
            return "https://adventofcode.com/2021/day/" + day + "/input";
        }

        public string SessionToken()
        {
            lock (sessionTokenLock)
            {
                try
                {
                    if (sessionToken == null)
                    {
                        sessionToken = File.ReadAllText(sessionTokenPath).Trim();
                    }
                    return sessionToken;
                } catch (Exception e)
                {
                    throw new Exception("Couldn't get session data from " + sessionTokenPath, e);
                }
            }
        }
    }
}
