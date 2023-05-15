using BattleShips.AI;
using BattleShips.Display;
using BattleShips.State;

Console.WriteLine("Welcome to Battleships!");
Console.WriteLine("---------------------------------------");

// TODO: entering game dimensions was outside of scope, needs validation for playing area max and minimum size
var dimensions = 10;

// Set up game state
var gameState = new GameState(dimensions);

// Set up display entities
var gameBoardDisplay = new GameBoard();
var shipsStatusDisplay = new ShipStatus();

// Add ships
var startingShipLengths = new List<int> {5,4,4};
foreach(var newShipLength in startingShipLengths) {
    var newShipCoordinates = ShipGenerator.GenerateShip(gameState.GetBoard(), newShipLength);    
    gameState.AddShip(newShipCoordinates);
}

var winConditionMet = gameState.WinConditionMet();

var shot = string.Empty;
var validationMessages = new List<ValidationState>();

// Main game loop
do {
    Console.Clear();

    if(validationMessages.Any()) {
        foreach(var message in validationMessages) {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message.ValidationMessage);
            Console.ResetColor();
        }
    }

    shipsStatusDisplay.Display(gameState.GetShips());
    gameBoardDisplay.Display(gameState.GetBoard());
        
    Console.Write("Enter your shot (to exit the game write the word 'quit'): ");
    shot = Console.ReadLine();

    validationMessages = ShotValidator.IsValidShot(shot, gameState.GetBoard()).ToList();

    if(!validationMessages.Any()) {
        gameState.TakeShot(shot);
    }

    winConditionMet = gameState.WinConditionMet();

} while (shot != "quit" && !winConditionMet);

// TODO: take this out into a display class like the game board and ship statuses
if (winConditionMet) {
    Console.Clear();

    shipsStatusDisplay.Display(gameState.GetShips());
    gameBoardDisplay.Display(gameState.GetBoard());

    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("You have won!");
    Console.ResetColor();
    Console.WriteLine("(press enter to exit)");
    Console.ReadLine();
}