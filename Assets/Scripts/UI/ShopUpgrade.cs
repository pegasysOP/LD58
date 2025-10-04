using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUpgrade : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI costText;
    public Button upgradeButton;

    private Upgrade upgrade;
    private float cost;

    private void OnEnable()
    {
        upgradeButton.onClick.AddListener(OnUpgradeButtonClick);
    }
    
    private void OnDisable()
    {
        upgradeButton.onClick.RemoveListener(OnUpgradeButtonClick);
    }

    public void Init(Upgrade upgrade, float cost)
    {
        this.upgrade = upgrade;
        this.cost = cost;

        nameText.text = upgrade.ToString();
        costText.text = "$" + cost.ToString("F2");
    }

    private void OnUpgradeButtonClick()
    {
        GameManager.Instance.inventory.DeductMoney(cost); // TODO: use shop buy method and check money first
    }
}
