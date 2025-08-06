namespace StrongPawns
{
    using Verse;
    using UnityEngine;

    public class StrongPawnsModSettings : ModSettings
    {
        private const float DefaultCarryingCapacity = 750f;
        public float CarryingCapacity = DefaultCarryingCapacity;

        public override void ExposeData()
        {
            Scribe_Values.Look(ref this.CarryingCapacity, nameof(this.CarryingCapacity), DefaultCarryingCapacity);
            base.ExposeData();
        }

        public void DoWindowContents(Rect inRect)
        {
            Listing_Standard listingStandard = new Listing_Standard();
            listingStandard.Begin(inRect);

            listingStandard.Label(
                "StrongPawns.CarryingCapacity.Title".Translate() + ": " + this.CarryingCapacity,
                tooltip: "StrongPawns.CarryingCapacity.Description".Translate());

            // Text field for numeric input
            string buffer = this.CarryingCapacity.ToString();
            listingStandard.TextFieldNumeric(ref this.CarryingCapacity, ref buffer, 10f, 14000f);

            listingStandard.End();
        }
    }
}