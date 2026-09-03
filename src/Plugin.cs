using BepInEx;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using Game.Data.Items.Models;
using HarmonyLib;
using Il2CppInterop.Runtime;

namespace WeightlessAmmo;

[BepInPlugin(PluginGuid, "Weightless Ammo", "1.0.0")]
public class Plugin : BasePlugin
{
    public const string PluginGuid = "com.ivmakk.tlsa.weightlessammo";
    internal static new ManualLogSource Log;

    public override void Load()
    {
        Log = base.Log;
        var harmony = new Harmony(PluginGuid);
        harmony.PatchAll();
        Log.LogInfo("Weightless Ammo loaded. Ranged-weapon ammo now weighs 0.");
    }
}

// Zero the weight at its source. The game reads the backing field m_Weight
// directly (il2cpp inlines the trivial Weight getter), so patching the getter
// has no effect. ItemModel.OnEnabling runs once per model as it loads; when the
// model is ammo, set m_Weight to 0 so every reader - encumbrance and UI - sees 0.
[HarmonyPatch(typeof(ItemModel), "OnEnabling")]
public static class ItemModelOnEnablingPatch
{
    private static int s_zeroed;

    [HarmonyPostfix]
    public static void Postfix(ItemModel __instance)
    {
        if (__instance.TryCast<AmmoItemModel>() == null)
        {
            return;
        }
        if (__instance.m_Weight != 0f)
        {
            Plugin.Log.LogDebug($"Zeroed ammo weight: {__instance.name} (was {__instance.m_Weight}), total {++s_zeroed}");
            __instance.m_Weight = 0f;
        }
    }
}

// Keep the getter postfix as a fallback for any code path that does call the
// property (non-inlined managed reads).
[HarmonyPatch(typeof(ItemModel), nameof(ItemModel.Weight), MethodType.Getter)]
public static class ItemModelWeightPatch
{
    [HarmonyPostfix]
    public static void Postfix(ItemModel __instance, ref float __result)
    {
        if (__instance.TryCast<AmmoItemModel>() != null)
        {
            __result = 0f;
        }
    }
}
