using TMPro;
using UnityEngine;
public class PlayerActionsController : MonoBehaviour
{
    [SerializeField]
    private GameObject PlanetInfoPanel;

    [SerializeField]
    private TMP_Text PlanetNameText, CurrencyGenText, TroopGenText, FuelGenText, MaterialGenText, SciencePoints, PlanetNotes;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
        TacMapGameEventsController.TacMapEvents.onPlanetHover += PlanetHover;
        TacMapGameEventsController.TacMapEvents.clearPlanetHover += ClearPlanetHover;
        PlanetInfoPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }   

    private void PlanetHover(PlanetController hoveredPlanet)
    {
        PlanetNameText.text = hoveredPlanet.GetPlanetName();

        ResourceVault planetResourceGeneration = hoveredPlanet.GetPlanetResources();

        CurrencyGenText.text = planetResourceGeneration.CurrencyCount * 60 + " / min";
        TroopGenText.text = planetResourceGeneration.ReserveTroopCount * 60 + " / min";
        FuelGenText.text = planetResourceGeneration.FuelCount * 60 + " / min";
        MaterialGenText.text = planetResourceGeneration.MaterialCount * 60 + " / min";
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
        PlanetNameText.text = "Planet Name";

        CurrencyGenText.text = "#### / min";
        TroopGenText.text = "#### / min";
        FuelGenText.text = "#### / min";
        MaterialGenText.text = "#### / min";
        SciencePoints.text = "# Points";

        PlanetNotes.text = "Troops: ####\nStructures:\n- XYZ\n- ABC";
        PlanetInfoPanel.SetActive(false);
    }
}
