using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace FirefightingControls
{
    class FirefightingUtilities
    {
        public static FloatMenu getFirefightingAreaMenu()
        {
            Map currentMap = Find.CurrentMap;
            MapComponent_FirefightingControls component = currentMap.GetComponent<MapComponent_FirefightingControls>();
            List<FloatMenuOption> firefightingAreasMenuOptions = new List<FloatMenuOption>();
            firefightingAreasMenuOptions.Add(new FloatMenuOption("Unrestricted", delegate { component.areaToFirefight = null; }));
            foreach (Area a in currentMap.areaManager.AllAreas)
            {
                firefightingAreasMenuOptions.Add(new FloatMenuOption(a.Label, delegate { component.areaToFirefight = a; }));
            }
            return new FloatMenu(firefightingAreasMenuOptions);
        }

        public static bool getIsEmergency()
        {
            return DefDatabase<WorkGiverDef>.GetNamed("FightFires").emergency;
        }

        public static void setIsEmergency(bool isEmergency)
        {
            DefDatabase<WorkGiverDef>.GetNamed("FightFires").emergency = isEmergency;
        }
    }
}
