using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace FirefightingControls
{
    class MapComponent_FirefightingControls : MapComponent
    {
        public Area areaToFirefight;
        public MapComponent_FirefightingControls(Map map) : base(map)
        {
            areaToFirefight = map.areaManager.Home;
            LongEventHandler.QueueLongEvent(ensureComponentExists, null, false, null);
        }

        public static void ensureComponentExists()
        {
            foreach (Map m in Find.Maps)
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
            else
            {
                return areaToFirefight[position];
            }
        }
    }
}
