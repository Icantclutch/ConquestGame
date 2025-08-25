using UnityEngine;

public class ResourceVault
{
    public int CurrencyCount;
    public int ActiveTroopCount;
    public int ReserveTroopCount;
    public float FuelCount;
    public float MaterialCount;
    public int SciencePoints;

    public ResourceVault()
    {
        CurrencyCount = 0;
        ActiveTroopCount = 0;
        ReserveTroopCount = 0;
        FuelCount = 0;
        MaterialCount = 0;
        SciencePoints = 0;           
    }

    public ResourceVault(int currencyCount, int troopCount, int reserveTroopCount, float fuelCount, float materialCount, int sciencePoints)
    {
        CurrencyCount = currencyCount;
        ActiveTroopCount = troopCount;
        ReserveTroopCount = reserveTroopCount;
        FuelCount = fuelCount;
        MaterialCount = materialCount;
        SciencePoints = sciencePoints;
    }
}
