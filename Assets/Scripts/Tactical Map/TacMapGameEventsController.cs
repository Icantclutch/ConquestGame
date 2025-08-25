using System;
using UnityEngine;

public class TacMapGameEventsController : MonoBehaviour
{
    public static TacMapGameEventsController TacMapEvents;

    private void Awake()
    {
        TacMapEvents = this;
    }

    public event Action<PlanetController> onPlanetHover;
    public event Action clearPlanetHover;



    public void PlanetHover(PlanetController hoveredPlanet)
    {
        if(onPlanetHover != null) onPlanetHover(hoveredPlanet);
    }

    public void ClearPlanetHover()
    {
        if(clearPlanetHover != null) clearPlanetHover();
    }

   
}
