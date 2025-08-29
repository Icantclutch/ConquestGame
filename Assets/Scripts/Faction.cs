using UnityEngine;

public class Faction
{
    private string FactionName;
    private Color FactionColor;

    public Faction()
    {
        FactionName = "Test Faction";
        FactionColor = Color.yellow;
    }

    public Faction(string factionName, Color factionColor)
    {
        FactionName = factionName;
        FactionColor = factionColor;
    }

    public Color GetFactionColor()
    {
        return FactionColor;
    }
}
