using UnityEngine;
using Verse;
using VFEC.Perks;

namespace VFEClassicalNoAutoResearch
{
    // Backs the bottom-right play-settings toggle. State lives in the global
    // Settings.AutoResearchEnabled, so the toggle and the settings checkbox stay in sync.
    public static class AutoResearchToggle
    {
        private static Texture2D iconFull;
        private static Texture2D iconEmpty;
        private static PerkDef profectusDef;

        // Full flask = auto research on, empty = off. ToggleableIcon draws the vanilla
        // green-check/red-X on top, so both the flask fill and the check/X show the state.
        public static Texture2D IconFull => iconFull ??= ContentFinder<Texture2D>.Get("UI/AutoResearchToggle_Full");
        public static Texture2D IconEmpty => iconEmpty ??= ContentFinder<Texture2D>.Get("UI/AutoResearchToggle_Empty");

        public static bool PerkObtained()
        {
            var game = Current.Game;
            if (game == null) return false;

            var manager = game.GetComponent<GameComponent_PerkManager>();
            if (manager?.ActivePerks == null) return false;

            profectusDef ??= DefDatabase<PerkDef>.GetNamedSilentFail("Profectus");
            return profectusDef != null && manager.ActivePerks.Contains(profectusDef);
        }

        public static void DrawToggle(WidgetRow row)
        {
            var settings = PatchMod.Settings;
            var enabled = settings.AutoResearchEnabled;
            var before = enabled;
            var tex = enabled ? IconFull : IconEmpty;

            row.ToggleableIcon(ref enabled, tex, "VFEC_NAR_ToggleTip".Translate());

            if (enabled == before) return;
            settings.AutoResearchEnabled = enabled;
            settings.SyncProfectusDescription();
            settings.Write();
        }
    }
}
