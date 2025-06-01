using System.Collections.Generic;
using HarmonyLib;
namespace AdjustableGameEconomy.Wrappers;

internal static class ShopManagerWrapper
{
    public static ShopManager instance { get; set; }
    
    private static AccessTools.FieldRef<ShopManager, T> GetFieldRef<T>(string fieldName) =>
        AccessTools.FieldRefAccess<ShopManager, T>(fieldName);

    private static readonly AccessTools.FieldRef<ShopManager, float> itemValueMultiplierRef =
        GetFieldRef<float>("itemValueMultiplier");

    private static readonly AccessTools.FieldRef<ShopManager, float> upgradeValueIncreaseRef =
        GetFieldRef<float>("upgradeValueIncrease");

    private static readonly AccessTools.FieldRef<ShopManager, float> healthPackValueIncreaseRef =
        GetFieldRef<float>("healthPackValueIncrease");

    private static readonly AccessTools.FieldRef<ShopManager, float> crystalValueIncreaseRef =
        GetFieldRef<float>("crystalValueIncrease");

    private static readonly AccessTools.FieldRef<ShopManager, List<ItemAttributes>> shoppingListRef =
        GetFieldRef<List<ItemAttributes>>("shoppingList");

    public static float itemValueMultiplier => itemValueMultiplierRef(instance);
    public static float upgradeValueIncrease => upgradeValueIncreaseRef(instance);
    public static float healthPackValueIncrease => healthPackValueIncreaseRef(instance);
    public static float crystalValueIncrease => crystalValueIncreaseRef(instance);

    public static List<ItemAttributes> shoppingList
    {
        get => shoppingListRef(instance);
        set => shoppingListRef(instance) = value;
    }
}