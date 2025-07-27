using RimWorld;
using Verse;

namespace StrongPawns
{
    using System;
    using System.Reflection;
    using HarmonyLib;

    [StaticConstructorOnStartup]
    public class StrongPawns
    {
        static StrongPawns()
        {
            var harmony = new Harmony("mpwoz.StrongPawns");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
            Log.Message("StrongPawns patch applied.");
        }

        public Harmony Harmony { get; set; }
    }

    [HarmonyPatch(typeof(MassUtility), nameof(MassUtility.Capacity))]
    public static class HarmonyPatches
    {
        public static void Postfix(ref float __result)
        {
            // context: we used to try to multiply the value here to factor in pawn size, etc. but that leads to display
            // issues, probably because this method gets called twice so we end up double multiplying (getting something
            // insane like 14000 instead of the desired ~700). So instead, just hardcode the value to 750 and call it a
            // day (unless it's zero, e.g. pawn incapacitated, etc.)
            if (__result > 0f) { __result = 750f; }
        }
    }
}