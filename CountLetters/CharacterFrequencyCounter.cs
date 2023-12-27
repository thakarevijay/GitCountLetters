using System.Collections.Concurrent;
using System.Text.RegularExpressions;

namespace CountLetters
{
    public class CharacterFrequencyCounter
    {
        static Dictionary<char, int> frequencyMap = new Dictionary<char, int>();
        static ConcurrentDictionary<char, int> frequencyConMap = new ConcurrentDictionary<char, int>();
        static Regex MyRegex = new Regex("[^a-z]", RegexOptions.IgnoreCase);

        public static async Task GetCharacterFrequency(string input)
        {
            var tasks = new List<Task>();
            var inputDat = MyRegex.Replace(input.ToLower(), string.Empty);
            foreach (char c in inputDat)
            {
                tasks.Add(Task.Run(() =>
                {
                    if (frequencyMap.ContainsKey(c))
                        frequencyMap[c]++;
                    else
                        frequencyMap[c] = 1;
                }
                ));
            }
            await Task.WhenAll(tasks);
        }

        public static Dictionary<char, int> LetterCount
        {
            get
            {
                return frequencyMap;
            }
        }

        public static ConcurrentDictionary<char, int> LetterCountCon
        {
            get
            {
                return frequencyConMap;
            }
        }

        static async Task<ConcurrentDictionary<char, int>> CountDistinctLettersAsync(string input)
        {
            var letterCounts = new ConcurrentDictionary<char, int>();
            var inputDat = MyRegex.Replace(input.ToLower(), string.Empty);

            var tasks = new List<Task>();

            foreach (char c in inputDat.ToLower())
            {
                tasks.Add(Task.Run(() =>
                {
                    letterCounts.AddOrUpdate(c, 1, (_, count) => count + 1);
                }));
            }
            await Task.WhenAll(tasks);
            return letterCounts;
        }

    }
}
