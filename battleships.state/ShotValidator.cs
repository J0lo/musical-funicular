using System.Text.RegularExpressions;
using BattleShips.Shared;

namespace BattleShips.State;
public static class ShotValidator {
    public static IEnumerable<ValidationState> IsValidShot(string? shotTaken, FieldState[,] board) {
        var size = board.GetLength(0);
        var maxYAxisValue = (char)(Consts.YDimensionMinimumValue + size);
        var validationStates = new List<ValidationState>();
        if (string.IsNullOrWhiteSpace(shotTaken)) {
            validationStates.Add(new ValidationState() { IsValid = false, ValidationMessage = "The shot is invalid, it cannot be an empty value" });
        
        } else if (shotTaken == Consts.ExitMessage) { 
            validationStates.Add(new ValidationState() { IsValid = false, ValidationMessage = "You are exiting the game." });
        } else {
            var match = Regex.Match(shotTaken, Consts.CoordinatesRegex);
            if (!match.Success) {
                validationStates.Add(new ValidationState() { IsValid = false, ValidationMessage = "The shot should be 2 or 3 charactes i.e.: 'A4', A10" });
            } else {
                var yAxis = match.Groups[1].Value.First();
                
                if(!Char.IsLetter(yAxis)) {
                    validationStates.Add(new ValidationState() { IsValid = false, ValidationMessage = "The shot should have Y axis described as a letter" });
                }

                if(Char.IsLetter(yAxis) && (yAxis < Consts.YDimensionMinimumValue || yAxis > maxYAxisValue)) {
                    validationStates.Add(new ValidationState() { IsValid = false, ValidationMessage = "The shot should have Y axis within the bounds of the game board" });
                }
                
                var xAxis = int.Parse(match.Groups[2].Value);
                if (xAxis <= 0 || xAxis > size) {
                    validationStates.Add(new ValidationState() { IsValid = false, ValidationMessage = "The shot should have X axis within the bounds of the game board" });
                }

                if (!validationStates.Any()) {
                    var shot = new Coordinates(shotTaken);

                    var fieldState = board[shot.XAxis, shot.YAxis];
                    if (fieldState == FieldState.Missed || fieldState == FieldState.Hit) {
                        validationStates.Add(new ValidationState() { IsValid = false, ValidationMessage = "The shot cannot be taken on an already hit or missed tile" });
                    }
                }
            }
        }

        return validationStates;
    }
}