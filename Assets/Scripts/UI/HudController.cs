using TMPro;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class HudController : MonoBehaviour
{
    public GameObject centreDot;
    public GameObject interactPrompt;

    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI batteryText;
    public ShopPanel shopPanel;

    public void UpdateMoneyText(float money)
    {
        moneyText.text = money.ToString("F2");
    }

    public void UpdateBatteryText(float battery)
    {
        
        batteryText.text = battery.ToString(battery + "%");
    }

    public void ShowShop(Shop shop)
    {
        shopPanel.OpenShop(shop);

        shopPanel.gameObject.SetActive(true);
    }
}
