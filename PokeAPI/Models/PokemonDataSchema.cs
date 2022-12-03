using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokeAPI.Models
{
    public class PokemonDataSchema
    {
        public int id { get; set; }
        public string? name { get; set; }

        public PokemonSpecies? species { get; set;}
    }

    public class PokemonSpecies
    {
        public string? name { get; set; }
        public string? url { get; set;}
    }
}