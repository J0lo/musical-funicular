using BattleShips.Shared;
using ConsoleTables;

namespace BattleShips.Display;

public class GameBoard {
    
    public void Display(FieldState[,] board) { 
        var yAxisNumber = Consts.YDimensionMinimumValue;
        
        var noOfRows = board.GetLength(0);
        var noOfColumns = board.GetLength(1);       
        
        var header = new List<string>();
        header.Add(string.Empty);
        for (var i = 1; i <= noOfColumns; i++) {
            header.Add(i.ToString());
        }

        var table = new ConsoleTable(header.ToArray());

        for (var i = 0; i < noOfRows; i++) {
            var row = new List<string>();

            row.Add(((char)(yAxisNumber + i)).ToString());
            for (var j = 0; j < noOfColumns; j++) {
                row.Add(board[i,j].GetDescription());
            }

            table.AddRow(row.ToArray());
        }

        table.Write();
     } 
}