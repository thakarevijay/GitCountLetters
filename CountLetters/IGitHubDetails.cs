namespace CountLetters
{
    public interface IGitHubDetails
    {
        Task<Directory> GetGitRepo(string owner, string name);
    }
}