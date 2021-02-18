using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace PrimeNumberTest
{
    static class Program
    {
        static HttpClient client = new HttpClient();

        static async Task Main(string[] args)
        {
            string uri = null;
            using (var fs = new FileStream("TestUri.json", FileMode.Open))
            {
                client.BaseAddress = new Uri((await JsonSerializer.DeserializeAsync<UriModel>(fs)).Uri);
            }
            await Test("", "200 Name Program: PrimesNumber | Author: Halych Ivan");
            await Test("primes/5", "200");
            await Test("primes/6", "404");
            await Test("primes?from=0&to=5", "200 2 3 5");
            await Test("primes?from=-5&to=1", "200");
            await Test("primes?&to=absd", "400 Invalid argument");

        }

        public static async Task Test(string uri,string expected)
        {
            var response = await client.GetAsync(uri);
            Console.WriteLine($"Send: {client.BaseAddress + uri}");
            Console.WriteLine($"Expected: \t{expected}");
            Console.WriteLine($"Get: \t\t{(int)response.StatusCode} { await response.Content.ReadAsStringAsync()}");
        }
    }
}
