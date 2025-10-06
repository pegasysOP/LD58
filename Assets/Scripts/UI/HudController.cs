using TMPro;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class HudController : MonoBehaviour
{
    public GameObject centreDot;
    public GameObject interactPrompt;

    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI batteryText;
    public InventoryPanel inventoryPanel;
    public ShopPanel shopPanel;
    public PauseMenu pauseMenu;

    public void UpdateMoneyText(float money)
    {
        moneyText.text = money.ToString("F2");
    }

    public void UpdateBatteryText(float battery)
    {
        if(battery < 0)
        {
            battery = 0;
        }

        batteryText.text = battery.ToString("F2");
    }

    public void UpdateInventory()
    {
        inventoryPanel.SetInventorySize(GameManager.Instance.inventory.maxSize);
        inventoryPanel.SetFilledSlots(GameManager.Instance.inventory.GetCurrentSize());
    }

    public void ShowShop(Shop shop)
    {
        shopPanel.OpenShop(shop);

        shopPanel.gameObject.SetActive(true);
    }

    public void ShowInteractPrompt(bool show)
    {
        interactPrompt.SetActive(show);
    }
}
