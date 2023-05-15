using BattleShips.State;

namespace BattleShips.AI;
public static class ShipGenerator
{
    public static List<Coordinates> GenerateShip(FieldState[,] board, int shipSize) {
        var boardSize = board.GetLength(0);
        var rnd = new Random();
        var xAxis = 0;
        var yAxis = 0;

        var allClear = true;
        var coordinates = new List<Coordinates>();

        // TODO: Add a limiter for this, currently it's not used so that this should be a problem, however when we consider variable
        // board dimensions, different amount of ships (with different lengths), it may be a pain to find a suitable space for the ship,
        // also we'd need to consider if there is a suitable space for the ship altogether. I'd propose to limit this loop to a set 
        // amount of tries, and after that we'd try any possibility that can be achievable for a viable ship placement
        do {
            coordinates = new List<Coordinates>();
            allClear = false;

            xAxis = rnd.Next(0,boardSize);
            yAxis = rnd.Next(0,boardSize);

            var directionRnd = (Direction)(new Random()).Next(0,3);
        
            switch (directionRnd) {
                case Direction.Up:
                    (allClear, coordinates) = CheckShipValidityAndReturnCoordinates(board, xAxis, yAxis, shipSize, x => x, y => y-1);
                    break;
                case Direction.Down:
                    (allClear, coordinates) = CheckShipValidityAndReturnCoordinates(board, xAxis, yAxis, shipSize, x => x, y => y+1);
                    break;
                case Direction.Left:
                    (allClear, coordinates) = CheckShipValidityAndReturnCoordinates(board, xAxis, yAxis, shipSize, x => x-1, y => y);
                    break;
                case Direction.Right:
                    (allClear, coordinates) = CheckShipValidityAndReturnCoordinates(board, xAxis, yAxis, shipSize, x => x+1, y => y);
                    break;
                default:
                    allClear = false;
                    break;
            }
        } while(!allClear);
        

        return coordinates;
    }

    private static (bool, List<Coordinates>) CheckShipValidityAndReturnCoordinates(FieldState[,] board, int startingXAxis, int startingYAxis, int shipSize, Func<int, int>? xAxisFunc = null, Func<int,int>? yAxisFunc = null) {
        var coordinates = new List<Coordinates>();
        if (shipSize == 0 ) {
            return (false, coordinates);
        }

        var xAxis = startingXAxis;
        var yAxis = startingYAxis;

        if (shipSize == 1) {
            coordinates.Add(new Coordinates() { XAxis = xAxis, YAxis = yAxis });
            return (board[xAxis, yAxis] == FieldState.Empty, coordinates);
        }

        var boardSize = board.GetLength(0);

        for (var i = 0; i < shipSize; i++) {
            if (xAxis < 0 
                || xAxis >= boardSize 
                || yAxis < 0 
                || yAxis >= boardSize 
                || board[xAxis, yAxis] != FieldState.Empty) {
                return (false, coordinates);
            }

            coordinates.Add(new Coordinates() { XAxis = xAxis, YAxis = yAxis });

            if (xAxisFunc != null) {
                xAxis = xAxisFunc(xAxis);
            }

            if (yAxisFunc != null) {
                yAxis = yAxisFunc(yAxis);
            }
        }

        return (true, coordinates);
    }
}
