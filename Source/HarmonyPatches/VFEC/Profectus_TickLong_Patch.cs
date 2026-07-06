using HarmonyLib;
using VFEC.Perks.Workers;

namespace VFEClassicalNoAutoResearch.HarmonyPatches.VFEC
{
    [HarmonyPatch(typeof(Profectus), nameof(Profectus.TickLong))]
    public static class Profectus_TickLong_Patch
    {
        public static bool Prefix() => PatchMod.Settings.AutoResearchEnabled;
    }
}
