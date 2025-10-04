using System.Collections.Generic;
using UnityEngine;

public class Upgrades : MonoBehaviour
{
    public enum Upgrade
    {
        Battery1,
        Battery2,
        Battery3, 
        Range1,
        Range2, 
        Range3
    }

    public Dictionary<Upgrade, float> upgradeCostDictionary = new Dictionary<Upgrade, float>();

    void Start()
    {
        upgradeCostDictionary[Upgrade.Battery1] = 1f;
        upgradeCostDictionary[Upgrade.Battery2] = 2f;
        upgradeCostDictionary[Upgrade.Battery3] = 4f;
        upgradeCostDictionary[Upgrade.Range1] = 1f;
        upgradeCostDictionary[Upgrade.Range2] = 2f;
        upgradeCostDictionary[Upgrade.Range3] = 4f;
    }
}
