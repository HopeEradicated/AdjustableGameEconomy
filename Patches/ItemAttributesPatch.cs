using System;
using HarmonyLib;
using UnityEngine;
using Photon.Pun;
using AdjustableGameEconomy.Wrappers;
using Random = UnityEngine.Random;

namespace AdjustableGameEconomy.Patches
{
    [HarmonyPatch(typeof(ItemAttributes), nameof(ItemAttributes.GetValue))]
    public class ItemAttributesPatch
    {
        public static bool Prefix(ItemAttributes __instance)
        {
            try
            {
                ItemAttributesWrapper.instance = __instance;
                if (GameManager.Multiplayer() && !PhotonNetwork.IsMasterClient)
                    return false;

                float value = CalculateBaseValue();
                value = ApplyMultipliers(value);
                ItemAttributesWrapper.value = Mathf.Max(1, (int)value);

                if (GameManager.Multiplayer() && PhotonNetwork.IsMasterClient)
                {
                    if (ItemAttributesWrapper.PhotonView == null)
                        AdjustableGameEconomyBase.Logger.LogFatal(" PhotonView is null — RPC will not be sent!");
                    else
                        ItemAttributesWrapper.PhotonView.RPC("GetValueRPC", RpcTarget.Others, ItemAttributesWrapper.value);
                }

                AdjustableGameEconomyBase.Logger.LogInfo($" '{ItemAttributesWrapper.itemAssetName ?? "<null>"}' of {ItemAttributesWrapper.itemType.ToString()} → Price: {value}");
            }
            catch (Exception ex)
            {
                AdjustableGameEconomyBase.Logger.LogFatal($" EXCEPTION during price calculation!\n" + $"Exception: {ex}\n" + $"StackTrace: {ex.StackTrace}");
            }
            return false;
        }
        
        private static float CalculateBaseValue()
        {
            float baseValue = Random.Range(ItemAttributesWrapper.itemValueMin, ItemAttributesWrapper.itemValueMax) * ShopManagerWrapper.itemValueMultiplier;
            if (Configuration.DisableCostRandomization.Value)
                baseValue = (ItemAttributesWrapper.itemValueMin + ItemAttributesWrapper.itemValueMax) * ShopManagerWrapper.itemValueMultiplier / 2;
            return baseValue < 1000f ? 1f : Mathf.Ceil(baseValue / 1000f);
        }

        private static float ApplyMultipliers(float num)
        {
            int index = Mathf.Clamp(SemiFunc.PlayerGetAll().Count - 1, 0, AdjustableGameEconomyBase.PlayerScaleData.Length - 1);
            float scaleFactor = AdjustableGameEconomyBase.PlayerScaleData[index];
            
            bool applyLevelScaling = !Configuration.DisableLevelsCompletedCostScale.Value && RunManager.instance != null;
            bool applyPurchaseScaling = !Configuration.DisableUpgradesPurchasedCostScale.Value && StatsManager.instance != null;

            switch (ItemAttributesWrapper.itemType)
            {
                case SemiFunc.itemType.item_upgrade:
                case SemiFunc.itemType.player_upgrade:
                    if (applyPurchaseScaling)
                    {
                        float multiplier = ShopManagerWrapper.upgradeValueIncrease * Configuration.PurchaseHistoryScaleMultiplier.Value;
                        num += num * multiplier * StatsManager.instance.GetItemsUpgradesPurchased(ItemAttributesWrapper.itemAssetName);
                    }
                    num = ApplyPlayerScaling(num, Configuration.UpgradePlayerCostScale.Value, scaleFactor);
                    num *= Configuration.UpgradePriceMultiplier.Value;
                    break;

                case SemiFunc.itemType.drone:
                    num = ApplyPlayerScaling(num, Configuration.DronePlayerCostScale.Value, scaleFactor);
                    num *= Configuration.DronePriceMultiplier.Value;
                    break;

                case SemiFunc.itemType.cart:
                case SemiFunc.itemType.pocket_cart:
                    num = ApplyPlayerScaling(num, Configuration.CartPlayerCostScale.Value, scaleFactor);
                    num *= Configuration.CartPriceMultiplier.Value;
                    break;

                case SemiFunc.itemType.power_crystal:
                    if (applyLevelScaling)
                    {
                        float multiplier = ShopManagerWrapper.crystalValueIncrease * Configuration.LevelsCompletedScaleMultiplier.Value;
                        num += num * multiplier * RunManager.instance.levelsCompleted;
                    }
                    num = ApplyPlayerScaling(num, Configuration.PowerCrystalPlayerCostScale.Value, scaleFactor);
                    num *= Configuration.PowerCrystalPriceMultiplier.Value;
                    break;

                case SemiFunc.itemType.grenade:
                case SemiFunc.itemType.mine:
                    num = ApplyPlayerScaling(num, Configuration.ExplosivePlayerCostScale.Value, scaleFactor);
                    num *= Configuration.ExplosivePriceMultiplier.Value;
                    break;

                case SemiFunc.itemType.melee:
                    num = ApplyPlayerScaling(num, Configuration.MeleePlayerCostScale.Value, scaleFactor);
                    num *= Configuration.MeleePriceMultiplier.Value;
                    break;

                case SemiFunc.itemType.healthPack:
                    if (applyLevelScaling)
                        num += num * ShopManagerWrapper.healthPackValueIncrease * RunManager.instance.levelsCompleted;
                    num = ApplyPlayerScaling(num, Configuration.HealthPackPlayerCostScale.Value, scaleFactor);
                    num *= Configuration.HealthPackPriceMultiplier.Value;
                    break;

                case SemiFunc.itemType.gun:
                    num = ApplyPlayerScaling(num, Configuration.GunPlayerCostScale.Value, scaleFactor);
                    num *= Configuration.GunPriceMultiplier.Value;
                    break;

                case SemiFunc.itemType.orb:
                    num = ApplyPlayerScaling(num, Configuration.OrbPlayerCostScale.Value, scaleFactor);
                    num *= Configuration.OrbPriceMultiplier.Value;
                    break;

                case SemiFunc.itemType.tracker:
                    num = ApplyPlayerScaling(num, Configuration.TrackerPlayerCostScale.Value, scaleFactor);
                    num *= Configuration.TrackerPriceMultiplier.Value;
                    break;
            }

            return num;
        }

        private static float ApplyPlayerScaling(float num, bool condition, float scaleFactor)
        {
            return condition && GameManager.Multiplayer() ? num * scaleFactor : num;
        }
    }
}
