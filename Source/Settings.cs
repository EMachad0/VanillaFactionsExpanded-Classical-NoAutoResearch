using UnityEngine;
using Verse;
using VFEC.Perks;

namespace VFEClassicalNoAutoResearch
{
    public class Settings : ModSettings
    {
        private const string DisabledSuffix = "\n(Disabled by mod setting)";

        public bool AutoResearchDisabled = true;

        private static string baseProfectusDescription;

        public void DoSettingsWindowContents(Rect inRect)
        {
            var wasDisabled = AutoResearchDisabled;

            var listing = new Listing_Standard();
            listing.Begin(inRect);
            listing.CheckboxLabeled("Disable auto research",
                ref AutoResearchDisabled,
                "Prevents the Eastern Republic senators' Profectus perk from automatically unlocking random research projects.");
            listing.End();

            if (AutoResearchDisabled != wasDisabled) SyncProfectusDescription();
        }

        public void SyncProfectusDescription()
        {
            var profectus = DefDatabase<PerkDef>.GetNamedSilentFail("Profectus");
            if (profectus == null) return;

            baseProfectusDescription ??= profectus.description;
            profectus.description = AutoResearchDisabled ? baseProfectusDescription + DisabledSuffix : baseProfectusDescription;
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref AutoResearchDisabled, "autoResearchDisabled", true);
        }
    }
}
