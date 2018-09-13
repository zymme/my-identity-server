using System;
using IdentityModel.Client;
using System.Threading;
using System.Threading.Tasks;

namespace MyIdentityClientApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello There! Grabbing the discovery info for  MyIdentityServer...");

            Program program = new Program();
            Thread.Sleep(3000); // arbitrary amount of time to allow multi project launch


            //program.GetTokenAsync().Wait();
            program.GetTokenResourceOwnerPasswordAsync().Wait();

            Console.WriteLine("Press ctrl+c to quit ...");

            Console.ReadLine();
        }




        private async Task GetTokenAsync()
        {
            var disco = await DiscoveryClient.GetAsync("http://localhost:5000");
            if(disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return;
            }

            // request token
            var tokenClient = new TokenClient(disco.TokenEndpoint, "client", "secret");
            var tokenResponse = await tokenClient.RequestClientCredentialsAsync("api1");

            if(tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }

            Console.WriteLine($"Token received: {tokenResponse.Json} ");

        }

        private async Task GetTokenResourceOwnerPasswordAsync()
        {
            var disco = await DiscoveryClient.GetAsync("http://localhost:5000");
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return;
            }

            var tokenClient = new TokenClient(disco.TokenEndpoint, "ro.client", "secret");
            var tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync("alice", "password", "api1");

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }

            Console.WriteLine(tokenResponse.Json);
            Console.WriteLine("\n\n");

        }
    }
}
