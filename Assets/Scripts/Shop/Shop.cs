using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shop : MonoBehaviour
{
    private Keyboard keyboard;

    private void Start()
    {
        keyboard = Keyboard.current;
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
        return GameManager.Instance.inventory.SellAll();

        // cursed, do not use
        //for (int i = 0; i < inventory.GetCurrentSize(); i++)
        //{
        //    Debug.Log($"Current size: {inventory.GetCurrentSize()}, i: {i}");
        //    inventory.RemoveItem();
        //}
    }

    private void BuyUpgrade(Upgrade upgrade) 
    {
        if(GameManager.Instance.inventory.GetMoney() >= GameManager.Instance.upgrades.upgradeCosts[upgrade])
        {
            GameManager.Instance.inventory.DeductMoney(GameManager.Instance.upgrades.upgradeCosts[upgrade]);

            ApplyUpgrade(upgrade);
        }

        Debug.Log(GameManager.Instance.inventory.GetMoney() + " Unable to afford upgrade. Better luck next time!");  
    }

    private void ApplyUpgrade(Upgrade upgrade)
    {
        switch (upgrade)
        {
            case Upgrade.Range:
                GameManager.Instance.metalDetector.range *= 2;
                Debug.Log("Range: " + GameManager.Instance.metalDetector.range);
                break;

            case Upgrade.Backpack:
                GameManager.Instance.inventory.maxSize += 1;
                Debug.Log(GameManager.Instance.inventory.maxSize);
                break;

            case Upgrade.WalletSize:
                GameManager.Instance.inventory.maxMoney *= 2;
                Debug.Log(GameManager.Instance.inventory.maxMoney);
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
