using BattleShips.State;

namespace BattleShips.State.Tests;

public class GameStateTests
{
    private GameState _gameState;

    [SetUp]
    public void Setup()
    {
        _gameState = new GameState(10);
    }

    [Test]
    public void ShouldRegisterShotInBoard()
    {
        _gameState.TakeShot("A5");
        var board = _gameState.GetBoard();
        
        Assert.That(board[0,4], Is.EqualTo(FieldState.Missed));
    }

    [Test] 
    public void ShouldMarkAsHitWhenShotTakenOnOccupiedField() {
        _gameState.AddShip(new List<Coordinates>() {new Coordinates() {XAxis = 0, YAxis = 4}});
        _gameState.TakeShot("A5");
        var board = _gameState.GetBoard();

        Assert.That(board[0,4], Is.EqualTo(FieldState.Hit));
    }

    [Test] 
    public void ShouldAddShipToProperFields() {
        _gameState.AddShip(new List<Coordinates>() {
            new Coordinates() {XAxis = 0, YAxis = 4},
            new Coordinates() {XAxis = 1, YAxis = 4},
            new Coordinates() {XAxis = 2, YAxis = 4},
            new Coordinates() {XAxis = 3, YAxis = 4},
            });
        
        var board = _gameState.GetBoard();

        Assert.That(board[0,4], Is.EqualTo(FieldState.Occupied));
        Assert.That(board[1,4], Is.EqualTo(FieldState.Occupied));
        Assert.That(board[2,4], Is.EqualTo(FieldState.Occupied));
        Assert.That(board[3,4], Is.EqualTo(FieldState.Occupied));
    }
}