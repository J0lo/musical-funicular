using BattleShips.State;

namespace BattleShips.State.Tests;

public class ShotValidatorTests
{
    private GameState _gameState;

    [SetUp]
    public void Setup()
    {
        _gameState = new GameState(10);
    }

    [Test]
    public void ShouldReturnValidWhenShotIsValid()
    {
        var validationPass = ShotValidator.IsValidShot("A5", _gameState.GetBoard());
        
        Assert.IsEmpty(validationPass);
    }

    [Test]
    public void ShouldReturnNotValidWhenShotIsNullOrEmptyOrWhitespace()
    {
        var validationNull = ShotValidator.IsValidShot(null, _gameState.GetBoard());
        var validationEmpty = ShotValidator.IsValidShot(string.Empty, _gameState.GetBoard());
        var validationWhitespace = ShotValidator.IsValidShot(" ", _gameState.GetBoard());

        
        Assert.IsNotEmpty(validationNull);
        Assert.IsNotEmpty(validationEmpty);
        Assert.IsNotEmpty(validationWhitespace);
    }

    [Test]
    public void ShouldReturnNotValidWhenXAxisIsZero()
    {
        var validationXAxisZero = ShotValidator.IsValidShot("A0", _gameState.GetBoard());
        
        Assert.IsNotEmpty(validationXAxisZero);
    }

    [Test]
    public void ShouldReturnNotValidWhenYAxisIsNotALetter()
    {
        var validationYAxisNotALetter = ShotValidator.IsValidShot("77", _gameState.GetBoard());
        
        Assert.IsNotEmpty(validationYAxisNotALetter);
    }

    [Test]
    public void ShouldReturnNotValidWhenXAxisIsOverGameDimension()
    {
        var validationXAxisOutOfBound = ShotValidator.IsValidShot("A11", _gameState.GetBoard());
        
        Assert.IsNotEmpty(validationXAxisOutOfBound);
    }

    [Test]
    public void ShouldReturnNotValidWhenYAxisIsOverGameDimension()
    {
        var validationYAxisOutOfBound = ShotValidator.IsValidShot("L7", _gameState.GetBoard());
        
        Assert.IsNotEmpty(validationYAxisOutOfBound);
    }

    [Test]
    public void ShouldReturnNotValidWhenAlreadyThisShotWasTaken()
    {
        var validationPassed = ShotValidator.IsValidShot("A7", _gameState.GetBoard());
        _gameState.TakeShot("A7");
        var validationNotPassed = ShotValidator.IsValidShot("A7", _gameState.GetBoard());
        
        Assert.IsEmpty(validationPassed);
        Assert.IsNotEmpty(validationNotPassed);
    }
}