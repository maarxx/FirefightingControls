using System.Reflection;
using HarmonyLib;
using Verse;

namespace FirefightingControls;

[StaticConstructorOnStartup]
internal class Main
{
    static Main()
    {
        var harmony = new Harmony("com.github.harmony.rimworld.maarx.firefightingcontrols");
        Log.Message("Hello from Harmony in scope: com.github.harmony.rimworld.maarx.firefightingcontrols");
        harmony.PatchAll(Assembly.GetExecutingAssembly());
    }
}