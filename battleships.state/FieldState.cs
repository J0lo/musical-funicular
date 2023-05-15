using System.ComponentModel;

public enum FieldState {
    [Description(" ")]
    Empty,
    [Description("O")]
    Missed,
    [Description(" ")]
    Occupied,
    [Description("X")]
    Hit,
}