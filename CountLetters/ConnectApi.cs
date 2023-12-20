using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountLetters
{
    public class ConnectApi
    {
        static async Task<List<FilesName>> GetUsers()
        {
            var users = new List<FilesName>();

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54741/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("api/user");
                response.EnsureSuccessStatusCode();
                //if (response.IsSuccessStatusCode)
                //{
                //    users = await response.Content.ReadAsStreamAsync<List<FilesName>>();
                //}
            }

            return users;
        }
    }
}
