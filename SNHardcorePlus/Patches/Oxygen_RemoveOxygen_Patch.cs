using Harmony;
using UnityEngine;

namespace SNHardcorePlus.Patches
{
    [HarmonyPatch(typeof(Oxygen))]
    [HarmonyPatch(nameof(Oxygen.RemoveOxygen))]
    class Oxygen_RemoveOxygen_Patch
    {
        public static void Prefix(ref float amount)
        {
            amount = amount * HCPSettings.Instance.OxygenDrainMultiplier;
        }
    }
}
