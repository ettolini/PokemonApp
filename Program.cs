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
                                var types = (JArray)dataObj["types"];
                                var typeNames = new string[2];
                                typeNames[0] = (string)types[0]["type"]["name"];
                                bool singleType = true;

                                if (types.Count > 1)
                                {
                                    typeNames[1] = (string)types[1]["type"]["name"];
                                    singleType = false;
                                }

                                PokeItem pokeItem = new PokeItem(
                                    id: $"{dataObj["id"]}",
                                    height: $"{dataObj["height"]}",
                                    weight: $"{dataObj["weight"]}",
                                    types: typeNames
                                );

                                string infoText = ($@"Id: {pokeItem.Id}
Height: {pokeItem.Height}
Weight: {pokeItem.Weight}
Types: {pokeItem.Types[0]}");
                                if (!singleType)
                                {
                                    infoText += $" & {pokeItem.Types[1]}";
                                }

                                //Log your pokeItem's name to the Console.
                                Console.WriteLine(infoText);
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