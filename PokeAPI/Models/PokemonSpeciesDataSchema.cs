using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokeAPI.Models
{
    public class PokemonSpeciesDataSchema
    {
        public bool is_legendary { get; set; }
        public PokemonSpeciesHabitat? habitat { get; set;}
        public PokemonSpeciesFlavorTextEntries[]? flavor_text_entries { get; set; }
    }

    public class PokemonSpeciesHabitat
    {
        public string? name { get; set; }
    }

    public class PokemonSpeciesFlavorTextEntries
    {
        public string? flavor_text { get; set; }
    }
}