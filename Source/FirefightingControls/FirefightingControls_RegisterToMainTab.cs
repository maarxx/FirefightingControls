using System;
using System.Collections.Generic;
using ModButtons;
using Verse;

namespace FirefightingControls;

internal class FirefightingControls_RegisterToMainTab
{
    public static bool wasRegistered;

    private static MapComponent_FirefightingControls getComponent()
    {
        return Find.CurrentMap.GetComponent<MapComponent_FirefightingControls>();
    }

    public static void ensureMainTabRegistered()
    {
        if (wasRegistered)
        {
            return;
        }

        Log.Message("Hello from FirefightingControls_RegisterToMainTab ensureMainTabRegistered");

        var columns = MainTabWindow_ModButtons.columns;

        var buttons = new List<ModButton_Text>
        {
            new ModButton_Text(
                () =>
                    $"Firefighting Area is Currently:{Environment.NewLine}{getComponent().areaToFirefight?.Label ?? "Unrestricted"}",
                delegate { Find.WindowStack.Add(FirefightingUtilities.getFirefightingAreaMenu()); }
            )
        };

        columns.Add(buttons);

        wasRegistered = true;
    }
}