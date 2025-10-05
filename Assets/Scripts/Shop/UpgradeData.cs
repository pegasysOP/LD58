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

public struct UpgradeData
{
    public Upgrade upgrade;
    public string name;
    public float cost;
}
