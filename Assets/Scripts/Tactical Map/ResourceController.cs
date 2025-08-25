using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceController : MonoBehaviour
{
    [SerializeField]
    private bool ActivePlayer;

    [SerializeField]
    private ResourceVault PlayerResources;

    [SerializeField]
    private TMP_Text CurrencyText, TroopText, FuelText, MaterialText, ScienceText;

    [SerializeField]
    private List<PlanetController> OwnedPlanets;

    private ResourceVault TotalPlanetResourceGeneration;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        //TODO this should generate from Faction Start
        PlayerResources = new ResourceVault();

        if (ActivePlayer)
        {
            CurrencyText.text = PlayerResources.CurrencyCount.ToString();
            TroopText.text = PlayerResources.ActiveTroopCount.ToString() + " : " + PlayerResources.ReserveTroopCount;
            FuelText.text = PlayerResources.FuelCount.ToString();
            MaterialText.text = PlayerResources.MaterialCount.ToString();
            ScienceText.text = PlayerResources.SciencePoints.ToString();
        }

        GetComponentInParent<TimeController>().GenerateResources.AddListener(GenerateGlobalResources);
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void GenerateGlobalResources()
    {
        /*
         * Grab list of owned planets
         * total resource generation
         * add to resources object
         * set text
         */

        PlayerResources.CurrencyCount += TotalPlanetResourceGeneration.CurrencyCount;       
        PlayerResources.ReserveTroopCount += TotalPlanetResourceGeneration.ReserveTroopCount;
        PlayerResources.FuelCount += TotalPlanetResourceGeneration.FuelCount;
        PlayerResources.MaterialCount += TotalPlanetResourceGeneration.MaterialCount;

        //Science and actvie troop counts are cumulative, not generative
        PlayerResources.ActiveTroopCount = TotalPlanetResourceGeneration.ActiveTroopCount;
        PlayerResources.SciencePoints = TotalPlanetResourceGeneration.SciencePoints;

        if (ActivePlayer)
        {
            CurrencyText.text = PlayerResources.CurrencyCount.ToString();
            TroopText.text = PlayerResources.ActiveTroopCount.ToString() + " : " + PlayerResources.ReserveTroopCount;
            FuelText.text = PlayerResources.FuelCount.ToString();
            MaterialText.text = PlayerResources.MaterialCount.ToString();
            ScienceText.text = PlayerResources.SciencePoints.ToString();
        }     
    }

    
    public void RecalculatePlanetaryResourceTotals()
    {
        int currencyTotal = 0;
        int activeTroopTotal = 0;
        int reserveTroopTotal = 0;
        float fuelTotal = 0;
        float materialTotal = 0;
        int scienceTotal = 0;

        foreach (PlanetController planet in OwnedPlanets)
        {
            ResourceVault planetResources = planet.GetPlanetResources();

            currencyTotal += planetResources.CurrencyCount;
            activeTroopTotal += planetResources.ActiveTroopCount;
            reserveTroopTotal += planetResources.ReserveTroopCount;
            fuelTotal += planetResources.FuelCount;
            materialTotal += planetResources.MaterialCount;
            scienceTotal += planetResources.SciencePoints;
        }

        TotalPlanetResourceGeneration.CurrencyCount = currencyTotal;
        TotalPlanetResourceGeneration.ActiveTroopCount = activeTroopTotal;
        TotalPlanetResourceGeneration.ReserveTroopCount = reserveTroopTotal;
        TotalPlanetResourceGeneration.FuelCount = fuelTotal;
        TotalPlanetResourceGeneration.MaterialCount = materialTotal;
        TotalPlanetResourceGeneration.SciencePoints = scienceTotal;
    }
}
