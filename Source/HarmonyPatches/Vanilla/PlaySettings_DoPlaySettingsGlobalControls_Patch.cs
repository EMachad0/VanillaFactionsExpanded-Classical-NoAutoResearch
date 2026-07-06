using HarmonyLib;
using RimWorld;
using Verse;

namespace VFEClassicalNoAutoResearch.HarmonyPatches.Vanilla
{
    // Appends the auto-research toggle to the bottom-right play-settings row
    [HarmonyPatch(typeof(PlaySettings), nameof(PlaySettings.DoPlaySettingsGlobalControls))]
    public static class PlaySettings_DoPlaySettingsGlobalControls_Patch
    {
        public static void Postfix(WidgetRow row, bool worldView)
        {
            if (worldView) return;
            if (!PatchMod.Settings.ShowInGameToggle) return;
            if (!AutoResearchToggle.PerkObtained()) return;

            AutoResearchToggle.DrawToggle(row);
        }
    }
}
