using UnityEngine;
using Verse;

namespace VFEClassicalNoAutoResearch
{
    public class Settings : ModSettings
    {
        public bool AutoResearchEnabled = true;
        public bool ShowInGameToggle = true;

        public void DoSettingsWindowContents(Rect inRect)
        {
            var listing = new Listing_Standard();
            listing.Begin(inRect);
            listing.CheckboxLabeled("VFEC_NAR_EnableAutoResearch".Translate(),
                ref AutoResearchEnabled,
                "VFEC_NAR_EnableAutoResearchTip".Translate());
            listing.CheckboxLabeled("VFEC_NAR_ShowToggle".Translate(),
                ref ShowInGameToggle,
                "VFEC_NAR_ShowToggleTip".Translate());
            listing.End();
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref AutoResearchEnabled, "autoResearchEnabled", true);
            Scribe_Values.Look(ref ShowInGameToggle, "showInGameToggle", true);
        }
    }
}
