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

        public PokedexService(IConfiguration config, IHttpClientFactory clientFactory)
        {
            _client = clientFactory.CreateClient("Pokedex");
            pokemonEndpoint = config["PokemonEndpoint"];
            pokemonSpeciesEndpoint = config["PokemonSpeciesEndpoint"];
        }

        public async Task<PokedexBasicResponse> ReturnBasicPokemonInfo(string name)
        {
            var pokemonData = await GetPokemonDataByName(name);
            var pokemonSpeciesData = await GetPokemonSpeciesDataByName(pokemonData.species.name);

            var allPokemonData = new PokedexBasicResponse
            {
                Name = name,
                Description = ReplaceCharsWithSpaces(pokemonSpeciesData.flavor_text_entries?.First().flavor_text),
                Habitat = pokemonSpeciesData.habitat?.name,
                IsLegendary = pokemonSpeciesData.is_legendary
            };

            return allPokemonData;
        }

        private async Task<PokemonDataSchema> GetPokemonDataByName(string name)
        {
            var request =  pokemonEndpoint + name;
            HttpResponseMessage response = await _client.GetAsync(request);
            var result = await response.Content.ReadAsStringAsync();

            if(response.IsSuccessStatusCode)
            {
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
            var request = pokemonSpeciesEndpoint + name;
            HttpResponseMessage response = await _client.GetAsync(request);
            var result = await response.Content.ReadAsStringAsync();

            if(response.IsSuccessStatusCode)
            {
                var pokemonSpecies = JsonSerializer.Deserialize<PokemonSpeciesDataSchema>(result);
                return pokemonSpecies;
            }
            else
            {
                throw new HttpRequestException($"Operation to retrieve Pokemon Species data has failed: {result}");
            } 
        }

        private string ReplaceCharsWithSpaces(string description)
        {
            description = description.Replace("\n", " ");
            return description.Replace("\f", " ");
        }
    }
}