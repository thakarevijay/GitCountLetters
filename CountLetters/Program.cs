// See https://aka.ms/new-console-template for more information
using CountLetters;

var task = GitHubDetails.GetGitRepo("tisonkun", "lodash");
task.Wait();
var dir = task.Result;

foreach (var item in CharacterFrequencyCounter.LetterCount.OrderBy(x => x.Key))
{
    Console.WriteLine($"Letter {item.Key} Count {item.Value}");
}

Console.ReadLine();