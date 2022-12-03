using Microsoft.AspNetCore.Mvc;
using PokeAPI.Models;
using PokeAPI.Interfaces;

namespace PokeAPI.Controllers;

[ApiController]
[Route("pokemon")]
public class PokedexController : ControllerBase
{
    private readonly ILogger<PokedexController> _logger;
    private readonly IPokedexService _pokedexService;

    public PokedexController(ILogger<PokedexController> logger, IPokedexService pokedexService)
    {
        _logger = logger;
        _pokedexService = pokedexService;
    }

    [HttpGet("{pokemonName}")]
    public async Task<ActionResult<PokemonSpeciesDataSchema>> GetBasicPokemonInformation(string pokemonName)
    {
        try
        {
            return Ok(await _pokedexService.GetPokemonDataByName(pokemonName));
        }
        catch (System.Exception)
        {
            
            throw;
        }

        return null;
    }
}
