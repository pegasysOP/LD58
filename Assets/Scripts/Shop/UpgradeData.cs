public enum Upgrade
{
    Battery,
    Range,
    Backpack,
    WalletSize,
    Rarity,
    Recharge,
    SolarPanel,
    SilverDetector,
    GoldDetector,
    HeartbeatSensor,
    Fossils,
    AlienTech
}

public struct UpgradeData
{
    public Upgrade upgrade;
    public string name;
    public float cost;
}
