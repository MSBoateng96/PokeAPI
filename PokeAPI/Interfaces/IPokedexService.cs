using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PokeAPI.Models;

namespace PokeAPI.Interfaces
{
    public interface IPokedexService
    {
        Task<PokedexResponse> ReturnBasicPokemonInfo(string name);
        Task<PokedexResponse> ReturnTranslatedPokemonInfo(string name);
    }
}