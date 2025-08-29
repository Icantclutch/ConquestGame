using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour
{
    [SerializeField]
    private string PlanetName;

    [SerializeField]
    private ResourceVault ResourceGeneration;

    [SerializeField]
    private ResourceVaultScriptable ResourcesScriptable;

    [SerializeField]
    private List<PlanetController> NeighboringPlanets;

    //TODO Could do Faction string instead
    [SerializeField]
    private int PlanetOwner; // 0 - Neutral, 1 - Faction 1, 2 - Faction 2, etc...

    //TODO probably should do enumeration here
    [SerializeField]
    private List<string> BuiltStructures;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ResourceGeneration = new ResourceVault(ResourcesScriptable.CurrencyCount, ResourcesScriptable.ActiveTroopCount, ResourcesScriptable.ReserveTroopCount,
            ResourcesScriptable.FuelCount, ResourcesScriptable.MaterialCount, ResourcesScriptable.SciencePoints);
        PlanetName = ResourcesScriptable.PlanetName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public ResourceVault GetPlanetResources()
    {
        return ResourceGeneration;
    }

    public string GetPlanetName()
    {
        return PlanetName;
    }

    public int GetPlanetOwner()
    {
        return PlanetOwner;
    }
    public List<string> GetBuiltStructures()
    {
        return BuiltStructures;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CameraViewTarget")
        {
            GameObject gameController = GameObject.FindGameObjectWithTag("GameController");

            Color outlineColor = new Color();

            switch (PlanetOwner)
            {
                case 1:
                    outlineColor = Variables.FactionOneColor; break;
                case 2:
                    outlineColor = Variables.FactionTwoColor; break;
                case 3:
                    outlineColor = Variables.FactionThreeColor; break;
                case 4:
                    outlineColor = Variables.FactionFourColor; break;
                default:
                    outlineColor = Color.black; break;
            }

            Outline outline = GetComponent<Outline>();
            outline.OutlineColor = outlineColor;
            outline.OutlineWidth = 3;
            outline.enabled = true;
            TacMapGameEventsController.TacMapEvents.PlanetHover(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "CameraViewTarget")
        {
            GetComponent<Outline>().enabled = false;
            TacMapGameEventsController.TacMapEvents.ClearPlanetHover();
        }
    }

  

}
