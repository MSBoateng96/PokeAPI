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
            return Ok(await _pokedexService.ReturnBasicPokemonInfo(name));
        }
        catch (Exception ex)
        {
            
            throw ex;
        }
    }

    [HttpGet("translated/{name}")]
    public async Task<ActionResult<PokedexResponse>> GetTranslatedPokemonInformation(string name)
    {
        try
        {
            return Ok(await _pokedexService.ReturnTranslatedPokemonInfo(name));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
