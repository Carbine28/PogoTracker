using System.Net.Http.Headers;
using System.Text.Json; // 

// Class to handle requests to the Pokemon Go API
namespace PogoTracker
{
    public class PoGoRequest
    {
        const string url = "https://pokemon-go-api.github.io/pokemon-go-api/";

        public async Task<PokemonDTO?> ProcessDexIdAsync(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                SetDefaultRequestHeaders(client);
                var res = await client.GetAsync(ConcatAndGetFullUrl("api/pokedex/id/" + id + ".json"));
                if (!res.IsSuccessStatusCode)
                {
                    return null;
                }
                await using Stream stream = await res.Content.ReadAsStreamAsync();
                //Console.WriteLine(test.StatusCode);
                //Console.WriteLine(await test.Content.ReadAsStringAsync());
                //await using Stream stream =
                //    await client.GetStreamAsync(ConcatAndGetFullUrl("api/pokedex/id/" + id + ".json"));
                
                return await JsonSerializer.DeserializeAsync<PokemonDTO>(stream);
            }
        }
        static string ConcatAndGetFullUrl(string appendUrl)
        {
            string completeUrl = url + appendUrl;
            return completeUrl;
        }

        static HttpClient SetDefaultRequestHeaders(HttpClient client)
        {
            client.DefaultRequestHeaders.Accept.Clear(); // Clear default headers
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); // Add header telling server to return JSON in the content type
            client.DefaultRequestHeaders.Add("User-Agent", "PogoRequest"); // Add header telling server who we are
            return client;
        }
    }
}
