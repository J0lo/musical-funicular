using BattleShips.AI;
using BattleShips.State;

namespace BattleShips.AI.Tests;

public class ShipGeneratorTests
{
    private FieldState[,] _board;
    [SetUp]
    public void Setup()
    {
        _board = new FieldState[10,10];

        for (var i = 0; i < 10; i++) {
            for (var j = 0; j < 10; j++) {
                _board[i,j] = FieldState.Empty;
            }
        }
    }

    [Test]
    public void ShouldCreateShipWhenEmptyBoard()
    {
        var shipSize = 5;
        var coordinates = ShipGenerator.GenerateShip(_board, shipSize);

        Assert.That(coordinates.Count, Is.EqualTo(shipSize));
    }

    [Test]
    public void ShouldCreateShipInOnlyHorizontalAvailableSpaceWhenBoardIsFull()
    {
        var shipSize = 5;
        
        for (var i = 0; i < 10; i++) {
            for (var j = 0; j < 10; j++) {
                if (j != 0 || i >= 5) {
                    _board[i,j] = FieldState.Missed;
                }
            }
        }
        
        var coordinates = ShipGenerator.GenerateShip(_board, shipSize);

        Assert.That(coordinates.Count, Is.EqualTo(shipSize));
        for (var i = 0; i < shipSize; i++) {
            Assert.True(coordinates.Any(c => c.XAxis == i && c.YAxis == 0));
        }
    }

    [Test]
    public void ShouldCreateShipInOnlyVerticalAvailableSpaceWhenBoardIsFull()
    {
        var shipSize = 5;
        
        for (var i = 0; i < 10; i++) {
            for (var j = 0; j < 10; j++) {
                if (i != 2 || j < 5) {
                    _board[i,j] = FieldState.Missed;
                }
            }
        }
        
        var coordinates = ShipGenerator.GenerateShip(_board, shipSize);

        Assert.That(coordinates.Count, Is.EqualTo(shipSize));
        for (var i = 0; i < shipSize; i++) {
            Assert.True(coordinates.Any(c => c.XAxis == 2 && c.YAxis == i+5));
        }
    }
}