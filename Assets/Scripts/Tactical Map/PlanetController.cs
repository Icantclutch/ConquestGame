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
    private int PlanetOwner; // 0 - Neutral, 1 - Player One, 2 - Player Two

    //TODO probably should do enumeration here
    [SerializeField]
    private List<string> BuiltStructures;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ResourceGeneration = new ResourceVault(ResourcesScriptable.CurrencyCount, ResourcesScriptable.ActiveTroopCount, ResourcesScriptable.ReserveTroopCount,
            ResourcesScriptable.FuelCount, ResourcesScriptable.MaterialCount, ResourcesScriptable.SciencePoints);
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

    public List<string> GetBuiltStructures()
    {
        return BuiltStructures;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CameraViewTarget")
        {
            GetComponent<Outline>().enabled = true;
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
