using HarmonyLib;
using AdjustableGameEconomy.Wrappers;
namespace AdjustableGameEconomy.Patches;

[HarmonyPatch(typeof(ShopManager), "Awake")]
public class ShopManagerPatch
{
    public static void Postfix(ShopManager __instance)
    {
        ShopManagerWrapper.instance = __instance;
    }
}