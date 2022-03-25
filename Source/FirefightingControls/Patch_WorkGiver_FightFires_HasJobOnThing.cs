using System.Reflection;
using HarmonyLib;
using RimWorld;
using Verse;
using Verse.AI;

namespace FirefightingControls;

[HarmonyPatch]
public class Patch_WorkGiver_FightFires_HasJobOnThing
{
    private static MethodBase TargetMethod()
    {
        return typeof(WorkGiver_FeedPatient).Assembly.GetType("RimWorld.WorkGiver_FightFires")
            .GetMethod("HasJobOnThing");
    }

    //public override bool HasJobOnThing(Pawn pawn, Thing t, bool forced = false)
    private static bool Prefix(Pawn pawn, Thing t, bool forced, ref bool __result)
    {
        //Log.Message("Hello from Patch_WorkGiver_FightFires_HasJobOnThing Prefix!");
        if (t is not Fire fire)
        {
            __result = false;
            return false;
        }

        var pawn2 = fire.parent as Pawn;
        var isFireInArea = isInFirefightingArea(fire.Map, fire.Position);
        if (pawn2 != null)
        {
            if (pawn2 == pawn)
            {
                __result = false;
                return false;
            }

            if ((pawn2.Faction == pawn.Faction || pawn2.HostFaction == pawn.Faction ||
                 pawn2.HostFaction == pawn.HostFaction) && !isFireInArea &&
                IntVec3Utility.ManhattanDistanceFlat(pawn.Position, pawn2.Position) > 15)
            {
                __result = false;
                return false;
            }

            if (!pawn.CanReach(pawn2, PathEndMode.Touch, Danger.Deadly))
            {
                __result = false;
                return false;
            }
        }
        else
        {
            if (pawn.WorkTagIsDisabled(WorkTags.Firefighting))
            {
                __result = false;
                return false;
            }

            if (!isFireInArea)
            {
                JobFailReason.Is(WorkGiver_FixBrokenDownBuilding.NotInHomeAreaTrans);
                __result = false;
                return false;
            }
        }

        if ((pawn.Position - fire.Position).LengthHorizontalSquared > 225 &&
            !pawn.CanReserve(fire, 1, -1, null, forced))
        {
            __result = false;
            return false;
        }

        if (FireIsBeingHandled(fire, pawn))
        {
            __result = false;
            return false;
        }

        //return true;
        __result = true;
        return false;
    }

    public static bool FireIsBeingHandled(Fire f, Pawn potentialHandler)
    {
        if (!f.Spawned)
        {
            return false;
        }

        return f.Map.reservationManager.FirstRespectedReserver(f, potentialHandler)?.Position
            .InHorDistOf(f.Position, 5f) ?? false;
    }

    public static bool isInFirefightingArea(Map map, IntVec3 position)
    {
        return map.GetComponent<MapComponent_FirefightingControls>().isInFirefightingArea(position);
    }
}