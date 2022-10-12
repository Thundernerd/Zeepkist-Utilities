using BepInEx;
using HarmonyLib;

namespace TNRD.Zeepkist.Utilities
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public sealed class Plugin : BaseUnityPlugin
    {
        private Harmony harmony;
        
        private void Awake()
        {
            harmony = new Harmony(PluginInfo.PLUGIN_GUID);
            harmony.PatchAll();

            gameObject.AddComponent<ClickScriptDisabler>();
            
            // Plugin startup logic
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
        }

        private void OnDestroy()
        {
            harmony.UnpatchSelf();
        }
    }
}
