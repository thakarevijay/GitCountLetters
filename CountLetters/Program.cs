using CountLetters;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = new ServiceCollection()
            .AddHttpClient()
            .AddTransient<IGitHubDetails, GitHubDetails>()
            .BuildServiceProvider();
var myApiService = serviceProvider.GetRequiredService<IGitHubDetails>();

var task = myApiService.GetGitRepo("tisonkun", "lodash");
task.Wait();
var dir = task.Result;

foreach (var item in CharacterFrequencyCounter.LetterCount.OrderBy(x => x.Key))
{
    Console.WriteLine($"Letter {item.Key} Count {item.Value}");
}

Console.ReadLine();