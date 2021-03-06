﻿using Harmony;
using System;

namespace SNHardcorePlus.Patches
{
    [HarmonyPatch(typeof(EnergyInterface))]
    [HarmonyPatch("ConsumeEnergy")]
    class EnergyInterface_ConsumeEnergy_Patch
    {
        public static void Postfix(EnergyInterface __instance, float __result, ref float amount)
        {
            float num = -__instance.ModifyCharge(-Math.Abs(amount * HCPSettings.Instance.PrawnSeamothPowerDrainMultiplier));
            __result = num;
        }
    }
}
