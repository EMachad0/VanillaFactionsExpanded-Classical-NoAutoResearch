using HarmonyLib;
using UnityEngine;
using Verse;

namespace VFEClassicalNoAutoResearch
{
    public class PatchMod : Mod
    {
        public static Settings Settings;

        public PatchMod(ModContentPack content) : base(content)
        {
            Settings = GetSettings<Settings>();

            LongEventHandler.ExecuteWhenFinished(delegate
            {
#if DEBUG
                Harmony.DEBUG = true;
#endif
                new Harmony("machado.vfeclassicalnoautoresearch").PatchAll();
            });
        }

        public override string SettingsCategory() => "VFEC_NAR_SettingsCategory".Translate();

        public override void DoSettingsWindowContents(Rect inRect)
        {
            Settings.DoSettingsWindowContents(inRect);
            Settings.Write();
        }
    }
}
