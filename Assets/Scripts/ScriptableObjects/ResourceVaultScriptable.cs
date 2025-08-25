using UnityEngine;

[CreateAssetMenu(fileName = "ResourceVaultScriptable", menuName = "Scriptable Objects/ResourceVaultScriptable")]
public class ResourceVaultScriptable : ScriptableObject
{
    public string PlanetName;

    public int CurrencyCount;
    public int ActiveTroopCount;
    public int ReserveTroopCount;
    public float FuelCount;
    public float MaterialCount;
    public int SciencePoints;

}
