using HarmonyLib;
using Photon.Pun;
namespace AdjustableGameEconomy.Wrappers;

internal static class ItemAttributesWrapper
{
    public static ItemAttributes instance { get; set; }

    private static AccessTools.FieldRef<ItemAttributes, T> GetFieldRef<T>(string fieldName) =>
        AccessTools.FieldRefAccess<ItemAttributes, T>(fieldName);

    private static readonly AccessTools.FieldRef<ItemAttributes, PhotonView> photonViewRef =
        GetFieldRef<PhotonView>("photonView");

    private static readonly AccessTools.FieldRef<ItemAttributes, string> itemAssetNameRef =
        GetFieldRef<string>("itemAssetName");

    private static readonly AccessTools.FieldRef<ItemAttributes, SemiFunc.itemType> itemTypeRef =
        GetFieldRef<SemiFunc.itemType>("itemType");

    private static readonly AccessTools.FieldRef<ItemAttributes, int> valueRef =
        GetFieldRef<int>("value");

    private static readonly AccessTools.FieldRef<ItemAttributes, float> itemValueMinRef =
        GetFieldRef<float>("itemValueMin");

    private static readonly AccessTools.FieldRef<ItemAttributes, float> itemValueMaxRef =
        GetFieldRef<float>("itemValueMax");

    public static PhotonView PhotonView => photonViewRef(instance);
    public static string itemAssetName => itemAssetNameRef(instance);
    public static SemiFunc.itemType itemType => itemTypeRef(instance);
    public static float itemValueMin => itemValueMinRef(instance);
    public static float itemValueMax => itemValueMaxRef(instance);

    public static int value
    {
        get => valueRef(instance);
        set => valueRef(instance) = value;
    }
}