using RimWorld;
using Verse;

namespace StrongPawns
{
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
            const float multiplier = 20f;
            __result *= multiplier;
        }
    }
}