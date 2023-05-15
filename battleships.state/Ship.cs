namespace BattleShips.State;

public class Ship {
    public ShipState State { get; set; }
    public List<Coordinates>? Coordinates { get; set; }
    public int HitPoints { get; set; }
}