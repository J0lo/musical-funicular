using System.ComponentModel;

public enum ShipState {
    [Description("Undamaged")]
    Undamaged,
    [Description("Partially Damaged")]
    PartialDamage,
    [Description("Sunk")]
    Sunk
}