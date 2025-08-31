using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class PlanetSelectionController : MonoBehaviour
{
    [SerializeField]
    private GameObject PlanetInfoPanel, StageInvasionPanel;

    [SerializeField]
    private TMP_Text PlanetNameText, CurrencyGenText, TroopGenText, FuelGenText, MaterialGenText, SciencePoints, PlanetNotes;
    
    [SerializeField]
    private Button StageInvasionButton, BuildStructuresButton, StageInvasionCancelButton, PreviousPlanetButton, NextPlanetButton;

    [SerializeField]
    private TMP_Text MaxTroopsText, StagedTroopsText, MaxFuelText, CurrentFuelText, DestinationPlanetText;

    [SerializeField]
    private Slider TroopSlider, FuelSlider;

    private PlanetController HoveredPlanet;

    [SerializeField]
    private ResourceController PlayerResources;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {     
        TacMapGameEventsController.TacMapEvents.onPlanetHover += PlanetHover;
        TacMapGameEventsController.TacMapEvents.clearPlanetHover += ClearPlanetHover;

        StageInvasionButton.onClick.AddListener(StageInvasion_Click);
        StageInvasionCancelButton.onClick.AddListener(CloseStageInvasionMenu);
        StageInvasionPanel.SetActive(false);

        PlanetInfoPanel.SetActive(false);

        TroopSlider.minValue = 50;
        FuelSlider.minValue = 0;
    }

    private void Update()
    {
        //If the stage invasion panel is open
        if (StageInvasionPanel.activeSelf)
        {
            UpdateInvasionUI();
         

        }
    }

    private void PlanetHover(PlanetController hoveredPlanet)
    {
        HoveredPlanet = hoveredPlanet;

        PlanetNameText.text = hoveredPlanet.GetPlanetName();

        ResourceVault planetResourceGeneration = hoveredPlanet.GetPlanetResources();

        CurrencyGenText.text = planetResourceGeneration.CurrencyCount + " / sec";
        TroopGenText.text = planetResourceGeneration.ReserveTroopCount + " / sec";
        FuelGenText.text = planetResourceGeneration.FuelCount + " / sec";
        MaterialGenText.text = planetResourceGeneration.MaterialCount + " / sec";
        SciencePoints.text = planetResourceGeneration.SciencePoints + " Points";

        PlanetNotes.text = $"Troops: {planetResourceGeneration.ActiveTroopCount}\nStructures:";
        foreach (string structure in hoveredPlanet.GetBuiltStructures()) 
        {
            PlanetNotes.text += $"\n- {structure}";
        }

        PlanetInfoPanel.SetActive(true);
    }

    private void ClearPlanetHover()
    {
        HoveredPlanet = null;
        PlanetNameText.text = "Planet Name";

        CurrencyGenText.text = "#### / min";
        TroopGenText.text = "#### / min";
        FuelGenText.text = "#### / min";
        MaterialGenText.text = "#### / min";
        SciencePoints.text = "# Points";

        PlanetNotes.text = "Troops: ####\nStructures:\n- XYZ\n- ABC";
        PlanetInfoPanel.SetActive(false);
    }

    private void StageInvasion_Click()
    {
        TroopSlider.minValue = 50;
        FuelSlider.minValue = 0;

        UpdateInvasionUI();

        StageInvasionPanel.SetActive(true);
    }

    private void CloseStageInvasionMenu()
    {
        StageInvasionPanel.SetActive(false);
    }

    private void UpdateInvasionUI()
    {
        TroopSlider.maxValue = HoveredPlanet.GetPlanetResources().ActiveTroopCount;
        MaxTroopsText.text = TroopSlider.maxValue.ToString();
        StagedTroopsText.text = TroopSlider.value.ToString();


        //TODO Calculate this based on the current troop count and the travel time
        FuelSlider.maxValue = PlayerResources.GetResourceVault().FuelCount;
        MaxFuelText.text = FuelSlider.maxValue.ToString();
        CurrentFuelText.text = FuelSlider.value.ToString();
    }
}
