using System;
//Using System.Net.Http directive which will enable HttpClient.
using System.Net.Http;
//use newtonsoft to convert json to c# objects.
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

        //Define your static method which will make the method become part of the class
        //Also make it asynchronous meaning it is retrieving data from a api.
        //Have it void since your are logging the result into the console.
        //Which would take a integar as a argument.
        public static async void GetPokemon(string pokeName)
        {
            //Define your base url
            string baseURL = $"http://pokeapi.co/api/v2/pokemon/{pokeName}/";
            //Have your api call in try/catch block.
            try
            {
                //Now we will have our using directives which would have a HttpClient 
                using (HttpClient client = new HttpClient())
                {
                    //Now get your response from the client from get request to baseurl.
                    //Use the await keyword since the get request is asynchronous, and want it run before next asychronous operation.
                    using (HttpResponseMessage res = await client.GetAsync(baseURL))
                    {
                        //Now we will retrieve content from our response, which would be HttpContent, retrieve from the response Content property.
                        using (HttpContent content = res.Content)
                        {
                            //Retrieve the data from the content of the response, have the await keyword since it is asynchronous.
                            string data = await content.ReadAsStringAsync();
                            //If the data is not null, parse the data to a C# object, then create a new instance of PokeItem.
                            if (data != null)
                            {
                                //Parse your data into a object.
                                var dataObj = JObject.Parse(data);
                                //Then create a new instance of PokeItem, and string interpolate your name property to your JSON object.
                                //Which will convert it to a string, since each property value is a instance of JToken.
                                var totalTypes = (JArray)dataObj["types"];
                                var types = totalTypes.Count > 1 ?
                                    $"{totalTypes[0]["type"]["name"]}, {totalTypes[1]["type"]["name"]}" :
                                    $"{totalTypes[0]["type"]["name"]}";

                                PokeItem pokeItem = new PokeItem(
                                    id: $"{dataObj["id"]}",
                                    height: $"{dataObj["height"]}",
                                    weight: $"{dataObj["weight"]}",
                                    types: $"{types}"
                                );
                                //Log your pokeItem's name to the Console.
                                Console.WriteLine(
                                    @"Id: {0}
Height: {1}
Weight: {2}
Types: {3}", pokeItem.Id, pokeItem.Height, pokeItem.Weight, pokeItem.Types);
                            }
                            else
                            {
                                //If data is null log it into console.
                                Console.WriteLine("Data is null!");
                            }
                        }
                    }
                }
                //Catch any exceptions and log it into the console.
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}