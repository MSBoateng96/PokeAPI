using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokeAPI.Models
{
    // Schema class for response from Fun Translations API: Yoda
    public class YodaTranslationDataSchema
    {
        public YodaTranslationContents contents;
    }

    public class YodaTranslationContents
    {
        public string? translated;
        public string? text;
        public string? translation;
    }
}