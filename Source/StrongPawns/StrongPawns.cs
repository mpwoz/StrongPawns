using RimWorld;
using Verse;

namespace StrongPawns
{
    using System;
    using System.Reflection;
    using HarmonyLib;
    using UnityEngine;

    public class StrongPawns : Mod
    {
        public static StrongPawnsModSettings Settings;

        public StrongPawns(ModContentPack content)
            : base(content)
        {
            Settings = this.GetSettings<StrongPawnsModSettings>();

            var harmony = new Harmony("mpwoz.StrongPawns");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
            Log.Message("StrongPawns patch applied.");
        }

        public Harmony Harmony { get; set; }

        public override string SettingsCategory() => "Strong Pawns";

        public override void DoSettingsWindowContents(Rect canvas)
        {
            Settings.DoWindowContents(canvas);
            base.DoSettingsWindowContents(canvas);
        }
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
            if (__result > 0f) { __result = StrongPawns.Settings.CarryingCapacity; }
        }
    }
}