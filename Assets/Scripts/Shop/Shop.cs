using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shop : MonoBehaviour
{
    private Keyboard keyboard;
    private Inventory inventory;
    private MetalDetector metalDetector;
    public Upgrades upgrades;

    private void Start()
    {
        keyboard = Keyboard.current;
        inventory = FindFirstObjectByType<Inventory>();
        metalDetector = FindFirstObjectByType<MetalDetector>();
        upgrades = FindFirstObjectByType<Upgrades>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (keyboard.eKey.wasPressedThisFrame)
            OpenShop();

        if (keyboard.digit1Key.wasPressedThisFrame)
            BuyUpgrade(Upgrade.Range);
    }

    private void OpenShop()
    {
        GameManager.Instance.hudController.ShowShop(this);
    }

    public float SellItems()
    {
        return inventory.SellAll();

        // cursed, do not use
        //for (int i = 0; i < inventory.GetCurrentSize(); i++)
        //{
        //    Debug.Log($"Current size: {inventory.GetCurrentSize()}, i: {i}");
        //    inventory.RemoveItem();
        //}
    }

    private void BuyUpgrade(Upgrade upgrade) 
    {
        if(inventory.GetMoney() >= upgrades.upgradeCosts[upgrade])
        {
            inventory.DeductMoney(upgrades.upgradeCosts[upgrade]);

            ApplyUpgrade(upgrade);
        }

        Debug.Log(inventory.GetMoney() + " Unable to afford upgrade. Better luck next time!");  
    }

    private void ApplyUpgrade(Upgrade upgrade)
    {
        switch (upgrade)
        {
            case Upgrade.Range:
                metalDetector.range *= 2;
                Debug.Log("Range: " + metalDetector.range);
                break;

            case Upgrade.Backpack:
                inventory.maxSize += 1;
                Debug.Log(inventory.maxSize);
                break;

            case Upgrade.WalletSize:
                inventory.maxMoney *= 2;
                Debug.Log(inventory.maxMoney);
                break;

            case Upgrade.Rarity:
                Debug.LogError("Upgrade " + upgrade.ToString() + " is not yet implemented");
                break;

            case Upgrade.GoldDetector:
                Debug.LogError("Upgrade " + upgrade.ToString() + " is not yet implemented");
                break;

            case Upgrade.HeartbeatSensor:
                Debug.LogError("Upgrade " + upgrade.ToString() + " is not yet implemented");
                break;

            case Upgrade.Fossils:
                Debug.LogError("Upgrade " + upgrade.ToString() + " is not yet implemented");
                break;

            case Upgrade.AlienTech:
                Debug.LogError("Upgrade " + upgrade.ToString() + " is not yet implemented");
                break;

            default:
                Debug.LogError("Upgrade " + upgrade.ToString() + " is not yet implemented");
                break;
        }
    }
} 
