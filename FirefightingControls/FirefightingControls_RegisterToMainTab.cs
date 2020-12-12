using ModButtons;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using HarmonyLib;
using System.Reflection;
using UnityEngine;

namespace FirefightingControls
{
    class FirefightingControls_RegisterToMainTab
    {
        private static MapComponent_FirefightingControls getComponent()
        {
            return Find.CurrentMap.GetComponent<MapComponent_FirefightingControls>();
        }

        public static bool wasRegistered = false;

        public static void ensureMainTabRegistered()
        {
            if (wasRegistered) { return; }

            Log.Message("Hello from FirefightingControls_RegisterToMainTab ensureMainTabRegistered");

            List<List<ModButton_Text>> columns = MainTabWindow_ModButtons.columns;

            List<ModButton_Text> buttons = new List<ModButton_Text>();

            buttons.Add(new ModButton_Text(
                delegate
                {
                    string buttonLabel = "Firefighting is Currently:" + Environment.NewLine;
                    if (FirefightingUtilities.getIsEmergency())
                    {
                        buttonLabel += "EMERGENCY";
                    }
                    else
                    {
                        buttonLabel += "Normal Work";
                    }
                    return buttonLabel;
                },
                delegate {
                    FirefightingUtilities.setIsEmergency(!FirefightingUtilities.getIsEmergency());
                }
            ));
            buttons.Add(new ModButton_Text(
                delegate
                {
                    return "Firefighting Area is Currently:" + Environment.NewLine + (getComponent().areaToFirefight?.Label ?? "Unrestricted");
                },
                delegate {
                    Find.WindowStack.Add(FirefightingUtilities.getFirefightingAreaMenu());
                }
            ));

            columns.Add(buttons);

            wasRegistered = true;
        }
    }
}
