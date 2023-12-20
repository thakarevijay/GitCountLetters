using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace CountLetters
{
    public class GitHubDetails
    {
        //Get all files from a repo
        public static async Task<Directory> GetGitRepo(string owner, string name)
        {
            using HttpClient client = new HttpClient();
            Directory root = await ReadGitDirectory(GitHubConstant.GitRoot, client, String.Format(GitHubConstant.GitPath, owner, name));
            return root;
        }

        //recursively get the contents of all files and subdirectories within a directory 
        private static async Task<Directory> ReadGitDirectory(String name, HttpClient client, string uri)
        {
            //get the directory contents
            using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri);
            request.Headers.Add(GitHubConstant.HeaderName, GitHubConstant.HeaderValue);

            //parse result
            using HttpResponseMessage response = await client.SendAsync(request);
            String jsonStr = await response.Content.ReadAsStringAsync(); ;
            GitFileInfo[] dirContents = JsonConvert.DeserializeObject<GitFileInfo[]>(jsonStr);

            //read in data
            Directory result;
            result.name = name;
            result.subDirs = new List<Directory>();
            result.files = new List<FileData>();
            foreach (GitFileInfo file in dirContents)
            {
                if (file.type == GitHubConstant.GitDir)
                { //read in the subdirectory
                    Directory sub = await ReadGitDirectory(file.name, client, file._links.self);
                    result.subDirs.Add(sub);
                }
                else if(file.type == GitHubConstant.GitFile && (new FileInfo(file.name).Extension == GitHubConstant.GitJSFile || new FileInfo(file.name).Extension == GitHubConstant.GitTSFile))
                { //get the file contents;
                    using HttpRequestMessage downLoadUrl = new HttpRequestMessage(HttpMethod.Get, file.download_url);
                    request.Headers.Add(GitHubConstant.HeaderName, GitHubConstant.HeaderValue);

                    using HttpResponseMessage contentResponse = await client.SendAsync(downLoadUrl);
                    String content = await contentResponse.Content.ReadAsStringAsync();

                    FileData data;
                    data.name = file.name;
                    data.contents = content;
                    await CharacterFrequencyCounter.GetCharacterFrequency(content).ConfigureAwait(false);

                    result.files.Add(data);
                }
            }
            return result;
        }
    }
}
