using System.Collections.Generic;
using UnityEngine;

public class Upgrades : MonoBehaviour
{
    public enum Upgrade
    {
        Battery1,
        Range1,
        Range2,
        Backpack1,
        WalletSize1,
        Rarity1,
        GoldDetector,
        HeartbeatSensor,
        Fossils, 
        AlienTech
    }

    public Dictionary<Upgrade, float> upgradeCostDictionary = new Dictionary<Upgrade, float>();

    void Start()
    {
        upgradeCostDictionary[Upgrade.Battery1] = 1f;
        upgradeCostDictionary[Upgrade.Range1] = 1f;
        upgradeCostDictionary[Upgrade.Range2] = 2f;
        upgradeCostDictionary[Upgrade.Backpack1] = 1f;
        upgradeCostDictionary[Upgrade.WalletSize1] = 1f;
        upgradeCostDictionary[Upgrade.Rarity1] = 1f;
        upgradeCostDictionary[Upgrade.GoldDetector] = 1f;
        upgradeCostDictionary[Upgrade.HeartbeatSensor] = 1f;
        upgradeCostDictionary[Upgrade.Fossils] = 1f;
        upgradeCostDictionary[Upgrade.AlienTech] = 1f;
    }
}
