using Newtonsoft.Json;
using RestSharp;
using System;
using System.Text.Json.Serialization;

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

            int choice;

            while (true)
            {
                Console.WriteLine("\nEscolha um número: ");
                if (!int.TryParse(Console.ReadLine(), out choice) && choice >= 1 && choice <= pokemonSpeciesResponse.Results.Count)
                {
                    Console.WriteLine("Escolha inválida. Tente novamente.");
                }
                else
                {
                    break;
                }
            }

            client = new RestClient($"https://pokeapi.co/api/v2/pokemon/{choice}");
            request = new RestRequest(Method.GET);
            response = client.Execute(request);

            var pokemonDetails = JsonConvert.DeserializeObject<PokemonDetailsResult>(response.Content);
            var chosenPokemon = pokemonSpeciesResponse.Results[choice -1];

            Console.WriteLine("\n");
            Console.WriteLine($"Você escolheu {chosenPokemon.Name}!");
            Console.WriteLine($"Detalhes:");
            Console.WriteLine($"- Nome: {chosenPokemon.Name}");
            Console.WriteLine($"- Peso: {pokemonDetails.Weight}");
            Console.WriteLine($"- Altura: {pokemonDetails.Height}");

            Console.WriteLine("\n Habilidades do Mascote: ");

            foreach (var abilityDetail in pokemonDetails.Abilities)
            {
                Console.WriteLine("Nome da Habilidade: " + abilityDetail.Ability.Name);
            }

            Console.WriteLine("\n");
        }
    }
}
