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

    [HttpGet("{name}")]
    public async Task<ActionResult<PokedexResponse>> GetBasicPokemonInformation(string name)
    {
        try
        {
            // Return 200 OK Response
            return Ok(await _pokedexService.ReturnBasicPokemonInfo(name));
        }
        catch (Exception ex)
        {
            // Return exception if there is an internal server error
            throw ex;
        }
    }

    [HttpGet("translated/{name}")]
    public async Task<ActionResult<PokedexResponse>> GetTranslatedPokemonInformation(string name)
    {
        try
        {
            // Return 200 OK Response
            return Ok(await _pokedexService.ReturnTranslatedPokemonInfo(name));
        }
        catch (Exception ex)
        {   
            // Return exception if there is an internal server error
            throw ex;
        }
    }
}
