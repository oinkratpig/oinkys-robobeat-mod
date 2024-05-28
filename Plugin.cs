using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;

namespace oinky_mod;

[BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
public class Plugin : BaseUnityPlugin
{
    new public static ManualLogSource Logger { get; private set; }
    Harmony _harmony = new Harmony(PluginInfo.PLUGIN_GUID);

    public static ConfigEntry<float> BossHealthMult { get; private set; }
    public static ConfigEntry<bool> FilmGrain { get; private set; }

    private void Awake()
    {
        Logger = base.Logger;
        Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loading...");

        BossHealthMult = Config.Bind("General", "BossHealthMult", 0.5f, "Multiplier for boss health. For example, a value of 0.5 means half.");
        FilmGrain = Config.Bind("General", "FilmGrain", false, "Enable/disable film grain.");

        _harmony.PatchAll(typeof(Patches));

        Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");

    } // end Awake

} // end class Plugin