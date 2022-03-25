using Verse;

namespace FirefightingControls;

internal class MapComponent_FirefightingControls : MapComponent
{
    public Area areaToFirefight;

    public MapComponent_FirefightingControls(Map map) : base(map)
    {
        areaToFirefight = map.areaManager.Home;
        LongEventHandler.QueueLongEvent(ensureComponentExists, null, false, null);
    }

    public static void ensureComponentExists()
    {
        foreach (var m in Find.Maps)
        {
            if (m.GetComponent<MapComponent_FirefightingControls>() == null)
            {
                m.components.Add(new MapComponent_FirefightingControls(m));
            }
        }
    }

    public bool isInFirefightingArea(IntVec3 position)
    {
        if (areaToFirefight == null)
        {
            return true;
        }

        return areaToFirefight[position];
    }
}