using HarmonyLib;
using System.Collections.Generic;

namespace oinky_mod;

/// <summary>
/// https://shorturl.at/ZxOqL
/// </summary>
internal class Patches
{
    private static List<string> _bosses = new();

    /// <summary>
    /// Visual changes.
    /// </summary>
    [HarmonyPatch(typeof(FMColor), "Start"), HarmonyPostfix]
    public static void FMColorPatch(FMColor __instance)
    {
        /* defaults
        film grain size = 1
        Plugin.Logger.LogInfo($"fresnel contribution: {__instance.FresnelSettings.Contribution}"); // 0
        Plugin.Logger.LogInfo($"fresnel scanline x: {__instance.FresnelSettings.FresnelScanlineX}"); // 0
        Plugin.Logger.LogInfo($"fresnel scanline y: {__instance.FresnelSettings.FresnelScanlineY}"); // 0
        Plugin.Logger.LogInfo($"fresnel power: {__instance.FresnelSettings.FresnelPower}"); // 1
        Plugin.Logger.LogInfo($"fxaa: {__instance.FXAASettings.Enable}"); // false
        Plugin.Logger.LogInfo($"scanline contribution: {__instance.ScanlineSettings.Contribution}"); // 0
        Plugin.Logger.LogInfo($"scanline x: {__instance.ScanlineSettings.ScanlineX}"); // 2
        Plugin.Logger.LogInfo($"scanline y: {__instance.ScanlineSettings.ScanlineY}"); // 1
        */

        // Film grain
        if (Plugin.FilmGrain.Value)
            __instance.GrainSettings.Size = 0f;

    } // end FMColorPatch

    /// <summary>
    /// Multiply boss health.
    /// </summary>
    [HarmonyPatch(typeof(UI_BossHP), "ConnectWithEntity"), HarmonyPrefix]
    public static void BossHealthMult(FMFilterGrain __instance, Entity entity)
    {
        if (!_bosses.Contains(entity.InternalName))
        {
            _bosses.Add(entity.InternalName);
            entity.Health.MaxHealth *= Plugin.BossHealthMult.Value;
            entity.Health.ScaleMaxHealth *= Plugin.BossHealthMult.Value;
            entity.Health.CurrentHealth *= Plugin.BossHealthMult.Value;
        }
        Plugin.Logger.LogInfo($"(OinkyMod boss spawned) scalemhp: {entity.Health.ScaleMaxHealth} mhp: {entity.Health.MaxHealth} hp: {entity.Health.CurrentHealth}");

    } // end BossHealthMult

} // end class Patches