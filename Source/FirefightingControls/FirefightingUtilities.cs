using System.Collections.Generic;
using Verse;

namespace FirefightingControls;

internal class FirefightingUtilities
{
    public static FloatMenu getFirefightingAreaMenu()
    {
        var currentMap = Find.CurrentMap;
        var component = currentMap.GetComponent<MapComponent_FirefightingControls>();
        var firefightingAreasMenuOptions = new List<FloatMenuOption>
            { new FloatMenuOption("Unrestricted", delegate { component.areaToFirefight = null; }) };
        foreach (var a in currentMap.areaManager.AllAreas)
        {
            firefightingAreasMenuOptions.Add(new FloatMenuOption(a.Label, delegate { component.areaToFirefight = a; }));
        }

        return new FloatMenu(firefightingAreasMenuOptions);
    }
}