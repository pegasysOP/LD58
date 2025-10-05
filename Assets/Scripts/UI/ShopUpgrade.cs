using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUpgrade : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI costText;
    public Button upgradeButton;

    private UpgradeData upgradeData;

    private void OnEnable()
    {
        upgradeButton.onClick.AddListener(OnUpgradeButtonClick);
    }
    
    private void OnDisable()
    {
        upgradeButton.onClick.RemoveListener(OnUpgradeButtonClick);
    }

    public void Init(UpgradeData upgradeData)
    {
        this.upgradeData = upgradeData;

        nameText.text = upgradeData.name;
        costText.text = "$" + upgradeData.cost.ToString("F2");
    }

    private void OnUpgradeButtonClick()
    {
        GameManager.Instance.inventory.DeductMoney(upgradeData.cost); // TODO: use shop buy method and check money first
    }
}
