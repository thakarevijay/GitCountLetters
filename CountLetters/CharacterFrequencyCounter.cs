using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CountLetters
{
    public class CharacterFrequencyCounter
    {
        static Dictionary<char, int> frequencyMap = new Dictionary<char, int>();
        static Regex MyRegex = new Regex("[^a-z]", RegexOptions.IgnoreCase);

        public static async Task GetCharacterFrequency(string input)
        {
            var inputDat = MyRegex.Replace(input.ToLower(), string.Empty);
            foreach (char c in inputDat)
            {
                if (frequencyMap.ContainsKey(c))
                    frequencyMap[c]++;
                else
                    frequencyMap[c] = 1;
            }
            await Task.Delay(400);

        }

        public static Dictionary<char, int> LetterCount { 
            get {
                return frequencyMap; 
            } 
        }
    }
}
