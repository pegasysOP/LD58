using System.Collections.Generic;
using UnityEngine;

public enum Upgrade
{
    Battery,
    Range,
    Backpack,
    WalletSize,
    Rarity,
    GoldDetector,
    HeartbeatSensor,
    Fossils,
    AlienTech
}

public class Upgrades : MonoBehaviour
{
    public Dictionary<Upgrade, float> upgradeCosts = new Dictionary<Upgrade, float>();

    public void Start()
    {
        upgradeCosts[Upgrade.Battery] = 1f;
        upgradeCosts[Upgrade.Range] = 1f;
        upgradeCosts[Upgrade.Backpack] = 1f;
        upgradeCosts[Upgrade.WalletSize] = 1f;
        upgradeCosts[Upgrade.Rarity] = 1f;
        upgradeCosts[Upgrade.GoldDetector] = 1f;
        upgradeCosts[Upgrade.HeartbeatSensor] = 1f;
        upgradeCosts[Upgrade.Fossils] = 1f;
        upgradeCosts[Upgrade.AlienTech] = 1f;
    }
}
