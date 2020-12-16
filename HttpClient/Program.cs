using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace HttpClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var client = new WebApiClient("http://localhost:5000/");

            var user = await client.GetAsync<User>("/Users", 1);
            await client.RefreshTokenAsync("/Auth", user);
            var users = await client.GetAsync<IEnumerable<User>>("/Users");
        }
    }
}
