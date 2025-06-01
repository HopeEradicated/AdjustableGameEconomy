using System.Globalization;
using System.Linq;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;
using AdjustableGameEconomy.Patches;
namespace AdjustableGameEconomy;

[BepInPlugin(PluginGuid, PluginName, PluginVersion)]
public class AdjustableGameEconomyBase : BaseUnityPlugin
{
    private const string PluginGuid = "HopeEradicated.REPO.AdjustableGameEconomy";
    private const string PluginName = "R.E.P.O. Adjustable Game Economy";
    private const string PluginVersion = "1.2.0";

    internal static AdjustableGameEconomyBase Instance { get; private set; }
    internal new static ManualLogSource Logger { get; private set; }
    internal static float[] PlayerScaleData { get; private set; }
    private readonly Harmony harmony = new Harmony(PluginGuid);
        
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        
        Logger = base.Logger;
        Configuration.Init(Config);
        
        harmony.PatchAll(typeof(AdjustableGameEconomyBase).Assembly);
        
        LoadPlayerScaleData();

        Logger.LogInfo($"{PluginName} v{PluginVersion} loaded");
        gameObject.hideFlags = HideFlags.DontSaveInEditor;
    }
    
    private void LoadPlayerScaleData()
    {
        string[] scaleValues = Configuration.PlayerScaleArray.Value.Split(',');
        PlayerScaleData = new float[scaleValues.Length];

        Logger.LogDebug("PlayerScaleData loaded - ");
        for (int i = 0; i < scaleValues.Length; i++)
        {
            if (float.TryParse(scaleValues[i], NumberStyles.Float, CultureInfo.InvariantCulture, out PlayerScaleData[i])) {
                Logger.LogDebug($"{PlayerScaleData[i]} ");
                continue;
            }
            Logger.LogError($"Invalid value in PlayerScaleArray: {scaleValues[i]}. Defaulting to 1.0");
            PlayerScaleData[i] = 1.0f;
            Logger.LogDebug($"!{PlayerScaleData[i]} ");
        }
    }
}

