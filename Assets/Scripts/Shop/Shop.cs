using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shop : MonoBehaviour
{
    private Keyboard keyboard;
    private Spawner spawner;

    private void Start()
    {
        keyboard = Keyboard.current;
        spawner = FindFirstObjectByType<Spawner>();
    }

    // Update is called once per frame
    private void Update()
    {
        // TODO: make this have to interact with shop object
        if (keyboard.eKey.wasPressedThisFrame)
            OpenShop();;
    }

    private void OpenShop()
    {
        GameManager.Instance.hudController.ShowShop(this);
    }

    public float SellItems()
    {
        return GameManager.Instance.inventory.SellAll();
    }

    private void BuyUpgrade(ref UpgradeData upgradeData) 
    {
        if(GameManager.Instance.inventory.GetMoney() >= upgradeData.cost)
        {
            GameManager.Instance.inventory.DeductMoney(upgradeData.cost);
            upgradeData.cost *= 1.5f; // increase cost for next time

            ApplyUpgrade(upgradeData.upgrade);
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
                foreach(Item item in spawner.items)
                {
                    //TODO: We probably want to do this in a better way
                    if(item.rarity == 1)
                    {
                        item.rarity *= 2;
                    }
                    else
                    {
                        item.rarity /= 2;
                    }
                }
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
