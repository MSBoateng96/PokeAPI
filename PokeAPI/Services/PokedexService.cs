using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using PokeAPI.Interfaces;
using PokeAPI.Models;

namespace PokeAPI.Services
{
    public class PokedexService : IPokedexService
    {
        private readonly HttpClient _client;
        private readonly string pokemonEndpoint;
        private readonly string pokemonSpeciesEndpoint;
        private readonly string yodaTranslationEndpoint;

        public PokedexService(IConfiguration config, IHttpClientFactory clientFactory)
        {
            // Create new HTTP client to aid REST API calls
            _client = clientFactory.CreateClient("Pokedex");

            // Obtain API endpoint string values from app settings config
            pokemonEndpoint = config["PokemonEndpoint"];
            pokemonSpeciesEndpoint = config["PokemonSpeciesEndpoint"];
            yodaTranslationEndpoint = config["YodaTranslationEndpoint"];
        }

        public async Task<PokedexResponse> ReturnBasicPokemonInfo(string name)
        {
            var pokemonData = await GetPokemonDataByName(name);
            var pokemonSpeciesData = await GetPokemonSpeciesDataByName(pokemonData.species.name);

            var allPokemonData = new PokedexResponse
            {
                Name = name,
                Description = ReplaceCharsWithSpaces(pokemonSpeciesData.flavor_text_entries?.First().flavor_text),
                Habitat = pokemonSpeciesData.habitat?.name,
                IsLegendary = pokemonSpeciesData.is_legendary
            };

            return allPokemonData;
        }

        public async Task<PokedexResponse> ReturnTranslatedPokemonInfo(string name)
        {
            var allPokemonData = await ReturnBasicPokemonInfo(name);

            var yodaTranslation = await GetYodaTranslation(allPokemonData.Description);
            string translatedDescription = yodaTranslation[9]; // SEE issue described in yodaTranslation()
            // string translatedDescription = yodaTranslation?.contents.translated;
            allPokemonData.Description = translatedDescription;

            return allPokemonData;
        }

        private async Task<PokemonDataSchema> GetPokemonDataByName(string name)
        {
            var request =  pokemonEndpoint + name; // Create URL request with parameter
            HttpResponseMessage response = await _client.GetAsync(request); // Send GET request to API
            var result = await response.Content.ReadAsStringAsync(); // Read request response as string

            if(response.IsSuccessStatusCode)
            {
                // Deserialize JSON response to PokemonDataSchema object
                var pokemon = JsonSerializer.Deserialize<PokemonDataSchema>(result);
                return pokemon;
            }
            else
            {
                throw new HttpRequestException($"Operation to retrieve Pokemon data has failed: {result}");
            }       
        }

        private async Task<PokemonSpeciesDataSchema> GetPokemonSpeciesDataByName(string name)
        {
            var request = pokemonSpeciesEndpoint + name; // Create URL request with parameter
            HttpResponseMessage response = await _client.GetAsync(request); // Send GET request to API
            var result = await response.Content.ReadAsStringAsync(); // Read request response as string

            if(response.IsSuccessStatusCode)
            {
                // Deserialize JSON response to PokemonSpeciesDataSchema object
                var pokemonSpecies = JsonSerializer.Deserialize<PokemonSpeciesDataSchema>(result);
                return pokemonSpecies;
            }
            else
            {
                throw new HttpRequestException($"Operation to retrieve Pokemon Species data has failed: {result}");
            } 
        }

        
        private async Task<string[]> GetYodaTranslation(string sentence) //private async Task<YodaTranslationDataSchema> GetYodaTranslation(string sentence)
        {
            var request = yodaTranslationEndpoint + $"?text={sentence}"; // Create URL request with parameter
            HttpResponseMessage response = await _client.GetAsync(request); // Send GET request to API
            var result = await response.Content.ReadAsStringAsync(); // Read request response as string

            if(response.IsSuccessStatusCode)
            {
                // Unable to deserialize Yoda API JSON as the raw format includes new line characters and whitespaces.
                // var yodaTranslation = JsonSerializer.Deserialize<YodaTranslationDataSchema>(result);
                // return yodaTranslation;

                //Temporary workaround: split string on " character
                var contents = result.Split('"');
                return contents;
            }
            else
            {
                throw new HttpRequestException($"Operation to retrieve Yoda Translation data has failed: {result}");
            } 
        }

        private string ReplaceCharsWithSpaces(string description)
        {
            description = description.Replace("\n", " ");
            return description.Replace("\f", " ");
        }
    }
}