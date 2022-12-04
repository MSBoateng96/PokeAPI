namespace PokeApiTest;

[TestClass]
public class TestPokeApiRequests
{
    private readonly Mock<IPokedexService> _pokedexService = new Mock<IPokedexService>();
    private readonly Mock<ILogger<PokedexController>> _logger = new Mock<ILogger<PokedexController>>();

    [TestMethod]
    public void TestBasicPokemonInfoSuccess()
    {
        var expectedResult = new PokedexResponse
        {
            Name = "clefairy",
            Description = "Its magical and cute appeal has many admirers. It is rare and found only in certain areas.",
            Habitat = "mountain",
            IsLegendary = false
        };

        _pokedexService.Setup(p => p.ReturnBasicPokemonInfo("clefairy")).Returns(Task.FromResult(expectedResult));

        var pokedexController = new PokedexController(_logger.Object, _pokedexService.Object);
        
        var result = pokedexController.GetBasicPokemonInformation("clefairy");
        var resultValue = result.Result.Value;

        Assert.AreEqual(expectedResult, resultValue);
    }

    public void TestTranslatedPokemonInfoSuccess()
    {
        var expectedResult = new PokedexResponse
        {
            Name = "clefairy",
            Description = "Many admirers,  its magical and cute appeal has.Rare and found only in certain areas,  it is.",
            Habitat = "mountain",
            IsLegendary = false
        };

        _pokedexService.Setup(p => p.ReturnBasicPokemonInfo("clefairy")).Returns(Task.FromResult(expectedResult));

        var pokedexController = new PokedexController(_logger.Object, _pokedexService.Object);
        
        var result = pokedexController.GetTranslatedPokemonInformation("clefairy");
        var resultValue = result.Result.Value;

        Assert.AreEqual(expectedResult, resultValue);
    }
}