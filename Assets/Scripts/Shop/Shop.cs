using NUnit.Framework;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shop : MonoBehaviour
{
    private Keyboard keyboard;
    private Inventory inventory;
    private MetalDetector metalDetector;
    private Upgrades upgrades;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        keyboard = Keyboard.current;
        inventory = FindFirstObjectByType<Inventory>();
        metalDetector = FindFirstObjectByType<MetalDetector>();
        upgrades = FindFirstObjectByType<Upgrades>();
    }

    // Update is called once per frame
    void Update()
    {
        if (keyboard.eKey.wasPressedThisFrame) OpenShop();

        if (keyboard.digit1Key.wasPressedThisFrame) BuyUpgrade(Upgrades.Upgrade.Range1);
    }

    void OpenShop()
    {
        for (int i = 0; i <= inventory.GetCurrentSize(); i++)
        {
            //FIXME: This should use < instead. But for some reason that isn't working. 
            Debug.Log("Current size: " + inventory.GetCurrentSize());
            inventory.RemoveItem();
        }
    }

    void BuyUpgrade(Upgrades.Upgrade upgrade) 
    {
        if(inventory.GetMoney() >= upgrades.upgradeCostDictionary[upgrade])
        {
            inventory.DeductMoney(upgrades.upgradeCostDictionary[upgrade]);

            ApplyUpgrade(upgrade);
        }

        Debug.Log(inventory.GetMoney() + " Unable to afford upgrade. Better luck next time!");  
    }

    void ApplyUpgrade(Upgrades.Upgrade upgrade)
    {
        switch (upgrade)
        {
            case Upgrades.Upgrade.Range1:
                metalDetector.range *= 2;
                Debug.Log("Range: " + metalDetector.range);
                break;

            case Upgrades.Upgrade.Range2:
                metalDetector.range *= 2;
                Debug.Log("Range: " + metalDetector.range);
                break;

            case Upgrades.Upgrade.Backpack1:
                inventory.maxSize += 1;
                Debug.Log(inventory.maxSize);
                break;

            case Upgrades.Upgrade.WalletSize1:
                inventory.maxMoney *= 2;
                Debug.Log(inventory.maxMoney);
                break;

            case Upgrades.Upgrade.Rarity1:
                Debug.LogError("Upgrade " + upgrade.ToString() + " is not yet implemented");
                break;

            case Upgrades.Upgrade.GoldDetector:
                Debug.LogError("Upgrade " + upgrade.ToString() + " is not yet implemented");
                break;

            case Upgrades.Upgrade.HeartbeatSensor:
                Debug.LogError("Upgrade " + upgrade.ToString() + " is not yet implemented");
                break;

            case Upgrades.Upgrade.Fossils:
                Debug.LogError("Upgrade " + upgrade.ToString() + " is not yet implemented");
                break;

            case Upgrades.Upgrade.AlienTech:
                Debug.LogError("Upgrade " + upgrade.ToString() + " is not yet implemented");
                break;

            default:
                Debug.LogError("Upgrade " + upgrade.ToString() + " is not yet implemented");
                break;
        }
    }
} 
