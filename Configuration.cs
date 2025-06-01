using BepInEx.Configuration;

namespace AdjustableGameEconomy
{
    internal static class Configuration
    {
        public static ConfigEntry<bool> UpgradePlayerCostScale;
        public static ConfigEntry<bool> HealthPackPlayerCostScale;
        public static ConfigEntry<bool> DronePlayerCostScale;
        public static ConfigEntry<bool> CartPlayerCostScale;
        public static ConfigEntry<bool> PowerCrystalPlayerCostScale;
        public static ConfigEntry<bool> ExplosivePlayerCostScale;
        public static ConfigEntry<bool> MeleePlayerCostScale;
        public static ConfigEntry<bool> GunPlayerCostScale;
        public static ConfigEntry<bool> OrbPlayerCostScale;
        public static ConfigEntry<bool> TrackerPlayerCostScale;

        public static ConfigEntry<bool> DisableCostRandomization;
        public static ConfigEntry<bool> DisableUpgradesPurchasedCostScale;
        public static ConfigEntry<bool> DisableLevelsCompletedCostScale;

        
        public static ConfigEntry<float> PurchaseHistoryScaleMultiplier;
        public static ConfigEntry<float> LevelsCompletedScaleMultiplier;
        public static ConfigEntry<float> UpgradePriceMultiplier;
        public static ConfigEntry<float> DronePriceMultiplier;
        public static ConfigEntry<float> CartPriceMultiplier;
        public static ConfigEntry<float> PowerCrystalPriceMultiplier;
        public static ConfigEntry<float> ExplosivePriceMultiplier;
        public static ConfigEntry<float> MeleePriceMultiplier;
        public static ConfigEntry<float> HealthPackPriceMultiplier;
        public static ConfigEntry<float> GunPriceMultiplier;
        public static ConfigEntry<float> OrbPriceMultiplier;
        public static ConfigEntry<float> TrackerPriceMultiplier;

        public static ConfigEntry<string> PlayerScaleArray;

        /*
         * Upgrade - item_upgrade, player_upgrade
         * Drone - drone
         * Cart - cart, pocket_cart
         * Power Crystal - power_crystal
         * Explosive Weapons - grenade, mine
         * Melee - melee
         * HealthPack - healthPack
         * Gun - gun
         * Orb - orb
         * Tracker - tracker
         */

        public static void Init(ConfigFile config)
        {
            // Cost scale toggles
            UpgradePlayerCostScale = config.Bind(
                "Toggles",
                "UpgradePlayerCostScale",
                false,
                "Enable cost scaling for upgrades based on player count."
            );

            HealthPackPlayerCostScale = config.Bind(
                "Toggles",
                "HealthPackPlayerCostScale",
                false,
                "Enable cost scaling for health packs based on player count."
            );

            DronePlayerCostScale = config.Bind(
                "Toggles",
                "DronePlayerCostScale",
                false,
                "Enable cost scaling for drones based on player count."
            );

            CartPlayerCostScale = config.Bind(
                "Toggles",
                "CartPlayerCostScale",
                false,
                "Enable cost scaling for carts based on player count."
            );

            PowerCrystalPlayerCostScale = config.Bind(
                "Toggles",
                "PowerCrystalPlayerCostScale",
                false,
                "Enable cost scaling for power crystals based on player count."
            );

            ExplosivePlayerCostScale = config.Bind(
                "Toggles",
                "ExplosivePlayerCostScale",
                false,
                "Enable cost scaling for explosive weapons based on player count."
            );

            MeleePlayerCostScale = config.Bind(
                "Toggles",
                "MeleePlayerCostScale",
                false,
                "Enable cost scaling for melee weapons based on player count."
            );

            GunPlayerCostScale = config.Bind(
                "Toggles",
                "GunPlayerCostScale",
                false,
                "Enable cost scaling for guns based on player count."
            );

            OrbPlayerCostScale = config.Bind(
                "Toggles",
                "OrbPlayerCostScale",
                false,
                "Enable cost scaling for orbs based on player count."
            );

            TrackerPlayerCostScale = config.Bind(
                "Toggles",
                "TrackerPlayerCostScale",
                false,
                "Enable cost scaling for trackers based on player count."
            );

            // Cost modifiers
            DisableCostRandomization = config.Bind(
                "General",
                "DisableCostRandomization",
                false,
                "Disable cost randomization, setting all costs to their average values."
            );

            DisableUpgradesPurchasedCostScale = config.Bind(
                "General",
                "DisableUpgradesPurchasedCostScale",
                false,
                "Disable cost scaling based on the number of upgrades purchased."
            );

            DisableLevelsCompletedCostScale = config.Bind(
                "General",
                "DisableLevelsCompletedCostScale",
                false,
                "Disable cost scaling based on the number of levels completed."
            );
            
            // Price multipliers
            PurchaseHistoryScaleMultiplier = config.Bind(
                "Multipliers",
                "PurchaseHistoryScaleMultiplier",
                1f,
                "Multiplier applied to existing purchase-scaling values (Upgrades)."
            );
            
            LevelsCompletedScaleMultiplier = config.Bind(
                "Multipliers",
                "LevelsCompletedScaleMultiplier",
                1f,
                "Multiplier applied to existing level-scaling values (Crystals, Health Packs)."
            );
            
            UpgradePriceMultiplier = config.Bind(
                "Multipliers",
                "UpgradePriceMultiplier",
                1f,
                "Multiplier for upgrade costs."
            );

            DronePriceMultiplier = config.Bind(
                "Multipliers",
                "DronePriceMultiplier",
                1f,
                "Multiplier for drone costs."
            );

            CartPriceMultiplier = config.Bind(
                "Multipliers",
                "CartPriceMultiplier",
                1f,
                "Multiplier for cart costs."
            );

            PowerCrystalPriceMultiplier = config.Bind(
                "Multipliers",
                "PowerCrystalPriceMultiplier",
                1f,
                "Multiplier for power crystal costs."
            );

            ExplosivePriceMultiplier = config.Bind(
                "Multipliers",
                "ExplosivePriceMultiplier",
                1f,
                "Multiplier for explosive weapon costs."
            );

            MeleePriceMultiplier = config.Bind(
                "Multipliers",
                "MeleePriceMultiplier",
                1f,
                "Multiplier for melee weapon costs."
            );

            HealthPackPriceMultiplier = config.Bind(
                "Multipliers",
                "HealthPackPriceMultiplier",
                1f,
                "Multiplier for health pack costs."
            );

            GunPriceMultiplier = config.Bind(
                "Multipliers",
                "GunPriceMultiplier",
                1f,
                "Multiplier for gun costs."
            );

            OrbPriceMultiplier = config.Bind(
                "Multipliers",
                "OrbPriceMultiplier",
                1f,
                "Multiplier for orb costs."
            );

            TrackerPriceMultiplier = config.Bind(
                "Multipliers",
                "TrackerPriceMultiplier",
                1f,
                "Multiplier for tracker costs."
            );

            PlayerScaleArray = config.Bind<string>(
                "ScaleArrays",
                "PlayerScaleArray",
                "1,2,2.5,3,4,4.5",
                "Array defining cost multiplier for different player counts (e.g., x4.5 for 6 players)."
            );
        }
    }
}
