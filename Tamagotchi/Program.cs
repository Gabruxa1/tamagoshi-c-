using Newtonsoft.Json;
using RestSharp;
using System;

namespace Tamagotchi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var client = new RestClient("https://pokeapi.co/api/v2/pokemon-species/");
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);

            var pokemonSpeciesResponse = JsonConvert.DeserializeObject<PokemonSpeciesResult>(response.Content);

            Console.WriteLine("Escolha um Tamagotchi");
            for (int i = 0; i < pokemonSpeciesResponse.Results.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {pokemonSpeciesResponse.Results[i].Name}");
            }

            int escolha;

            while (true)
            {
                Console.WriteLine("\nEscolha um número: ");
                if (!int.TryParse(Console.ReadLine(), out escolha) && escolha >= 1 && escolha <= pokemonSpeciesResponse.Results.Count)
                {
                    Console.WriteLine("Escolha inválida. Tente novamente.");
                }
                else
                {
                    break;
                }
            }

            client = new RestClient($"https://pokeapi.co/api/v2/pokemon/{escolha}");
            request = new RestRequest(Method.GET);
            response = client.Execute(request);
            Console.WriteLine(response.Content);
        }
    }
}
