using System.Reflection;
using LudeonTK;
using Verse;
using VFEC.Perks;
using VFEC.Perks.Workers;

namespace VFEClassicalNoAutoResearch
{
    // Dev-mode helper to inspect where the mod stands
    public static class DebugActions
    {
        private const string Category = "VFE Classical - No Auto Research";
        private const int TicksPerDay = 60000; // Profectus INTERVAL

        private static readonly FieldInfo NextResearchTickField =
            typeof(Profectus).GetField("nextResearchTick", BindingFlags.Instance | BindingFlags.NonPublic);
        private static readonly FieldInfo ResearchesUnlockedField =
            typeof(Profectus).GetField("researchesUnlocked", BindingFlags.Instance | BindingFlags.NonPublic);

        private static Profectus Worker =>
            DefDatabase<PerkDef>.GetNamedSilentFail("Profectus")?.Worker as Profectus;

        [DebugAction(Category, "Log Profectus status", allowedGameStates = AllowedGameStates.PlayingOnMap)]
        private static void LogProfectusStatus()
        {
            var enabled = PatchMod.Settings.AutoResearchEnabled;
            var obtained = AutoResearchToggle.PerkObtained();
            var worker = Worker;

            if (!obtained || worker == null)
            {
                Log.Message($"[No Auto Research] perk obtained: {obtained}, setting enabled: {enabled}. " +
                            "Perk not active, so nothing is scheduled yet.");
                return;
            }

            var next = (int)NextResearchTickField.GetValue(worker);
            var unlocked = (int)ResearchesUnlockedField.GetValue(worker);
            var remaining = next - Find.TickManager.TicksGame;
            Log.Message($"[No Auto Research] setting enabled: {enabled}, researches unlocked so far: {unlocked}, " +
                        $"next unlock in {remaining} ticks (~{remaining / (float)TicksPerDay:0.0} days).");
        }
    }
}
