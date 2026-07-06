using UnityEngine;
using Verse;
using VFEC.Perks;

namespace VFEClassicalNoAutoResearch
{
    public class Settings : ModSettings
    {
        public bool AutoResearchEnabled = true;
        public bool ShowInGameToggle = true;

        private static string baseProfectusDescription;

        public void DoSettingsWindowContents(Rect inRect)
        {
            var wasEnabled = AutoResearchEnabled;

            var listing = new Listing_Standard();
            listing.Begin(inRect);
            listing.CheckboxLabeled("VFEC_NAR_EnableAutoResearch".Translate(),
                ref AutoResearchEnabled,
                "VFEC_NAR_EnableAutoResearchTip".Translate());
            listing.CheckboxLabeled("VFEC_NAR_ShowToggle".Translate(),
                ref ShowInGameToggle,
                "VFEC_NAR_ShowToggleTip".Translate());
            listing.End();

            if (AutoResearchEnabled != wasEnabled) SyncProfectusDescription();
        }

        public void SyncProfectusDescription()
        {
            var profectus = DefDatabase<PerkDef>.GetNamedSilentFail("Profectus");
            if (profectus == null) return;

            baseProfectusDescription ??= profectus.description;
            profectus.description = AutoResearchEnabled
                ? baseProfectusDescription
                : baseProfectusDescription + "VFEC_NAR_DisabledSuffix".Translate();
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref AutoResearchEnabled, "autoResearchEnabled", true);
            Scribe_Values.Look(ref ShowInGameToggle, "showInGameToggle", true);
        }
    }
}
