public enum Upgrade
{
    Battery,
    Range,
    Backpack,
    WalletSize,
    Rarity,
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
