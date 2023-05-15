using BattleShips.Shared;

namespace BattleShips.State;

public class GameState 
{
    private int _size;
    private FieldState[,] _board;
    private char _maxYAxisValue;
    private List<Ship> _ships;

    public GameState(int size) {
        _size = size;
        _maxYAxisValue = (char)(Consts.YDimensionMinimumValue + size - 1);

        _ships = new List<Ship>();
        _board = new FieldState[_size, _size];

        for (var i = 0; i < _size; i++) {
            for (var j = 0; j < _size; j++) {
                _board[i,j] = FieldState.Empty;
            }
        }
    }

    public void AddShip(List<Coordinates> coordinates) { 
        var newShip = new Ship() {
            State = ShipState.Undamaged,
            Coordinates = coordinates,
            HitPoints = coordinates.Count
        };

        _ships.Add(newShip);

        foreach(var coordinate in coordinates) {
            _board[coordinate.XAxis, coordinate.YAxis] = FieldState.Occupied;
        }
    }

    public void TakeShot(string? shotTaken) {
        var shot = new Coordinates(shotTaken);
        var fieldState = _board[shot.XAxis, shot.YAxis];

        if (_board[shot.XAxis, shot.YAxis] == FieldState.Empty) {
            _board[shot.XAxis, shot.YAxis] = FieldState.Missed;
        } else if (_board[shot.XAxis, shot.YAxis] == FieldState.Occupied) {
            _board[shot.XAxis, shot.YAxis] = FieldState.Hit;
            
            var hitShip = _ships.Single(s => s.Coordinates.Any(c => c.XAxis == shot.XAxis && c.YAxis == shot.YAxis));
            hitShip.HitPoints--;
            hitShip.State = hitShip.HitPoints == 0 ? ShipState.Sunk : ShipState.PartialDamage;
        }
    }

    public FieldState[,] GetBoard() {
        return _board;
    }

    public List<Ship> GetShips() {
        return _ships;
    }

    public bool WinConditionMet() {
        if (_ships.All(s => s.State == ShipState.Sunk)) {
            return true;
        }

        return false;
    }
}