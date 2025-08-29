using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class PlayerActionsController : MonoBehaviour
{
    [SerializeField]
    private GameObject PlanetInfoPanel;

    [SerializeField]
    private TMP_Text PlanetNameText, CurrencyGenText, TroopGenText, FuelGenText, MaterialGenText, SciencePoints, PlanetNotes;

    [SerializeField]
    private Button DEBUGCycleOwnerButton, DEBUGSimulateCombatButton, StageInvasionButton, BuildStructuresButton;

    private PlanetController HoveredPlanet;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StageInvasionButton.onClick.AddListener(StageInvasion_Click);


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

    }
}
