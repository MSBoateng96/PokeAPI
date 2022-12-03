using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PokeAPI.Models;

namespace PokeAPI.Interfaces
{
    public interface IPokedexService
    {
        Task<PokemonDataSchema> GetPokemonDataByName(string name);
        Task<PokemonSpeciesDataSchema> GetPokemonSpeciesDataByName(string name);
    }
}