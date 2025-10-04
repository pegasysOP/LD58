using TMPro;
using UnityEngine;

public class HudController : MonoBehaviour
{
    public GameObject centreDot;
    public GameObject interactPrompt;

    public TextMeshProUGUI moneyText;
    public ShopPanel shopPanel;

    public void UpdateMoneyText(float money)
    {
        moneyText.text = money.ToString("F2");
    }

    public void ShowShop(Shop shop)
    {
        shopPanel.OpenShop(shop);

        shopPanel.gameObject.SetActive(true);
    }
}
