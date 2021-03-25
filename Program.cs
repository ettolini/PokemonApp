using System;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PokemonApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please, enter a Pokemon's name: ");
            string input = Console.ReadLine();

            Console.WriteLine("...");
            GetPokemon(input);
            Console.ReadLine();
        }

        public static async void GetPokemon(string pokeName)
        {
            try
            {
                HttpClient client = new HttpClient() { BaseAddress = new Uri("http://pokeapi.co/api/v2/pokemon/") };
                string url = $"{pokeName}/";

                client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "text/html,application/json");

                var response = await client.GetAsync(url);
                var content = await response.Content.ReadAsStringAsync();

                PokeItem pokeItem = JsonConvert.DeserializeObject<PokeItem>(content);

                // TODO: use master branch to check the type of the "types" element inside the json
                System.Console.WriteLine($@"Id: {pokeItem.Id}
Height: {pokeItem.Height}
Weight: {pokeItem.Weight}
Types: {pokeItem.Types[0]["type"]["name"]}");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                System.Console.WriteLine("HELP: Maybe you entered a non-existing Pokémon name...?");
            }
        }
    }
}