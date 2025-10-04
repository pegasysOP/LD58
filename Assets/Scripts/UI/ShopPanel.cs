using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ShopPanel : MonoBehaviour
{
    public Button closeButton;
    public Button sellButton;
    public Transform upgradeComponentContainer;
    public ShopUpgrade upgradeComponentPrefab;

    private List<GameObject> upgradeComponents = new List<GameObject>();

    private Shop currentShop;

    private void OnEnable()
    {
        closeButton.onClick.AddListener(CloseShop);
        sellButton.onClick.AddListener(OnSellButtonClick);
    }
    
    private void OnDisable()
    {
        closeButton.onClick.RemoveListener(CloseShop);
        sellButton.onClick.RemoveListener(OnSellButtonClick);
    }

    private void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame)
            CloseShop();
    }

    public void OpenShop(Shop shop)
    {
        currentShop = shop;

        foreach (KeyValuePair<Upgrade, float> upgradeCost in GameManager.Instance.upgrades.upgradeCosts)
        {
            ShopUpgrade upgradeComponent = Instantiate(upgradeComponentPrefab, upgradeComponentContainer);
            upgradeComponent.Init(upgradeCost.Key.ToString(), upgradeCost.Value); // TODO: replace with proper upgrade name
            upgradeComponents.Add(upgradeComponent.gameObject);
        }
    }

    public void CloseShop()
    {
        foreach (GameObject upgradeComponent in upgradeComponents)
            Destroy(upgradeComponent);

        upgradeComponents.Clear();

        gameObject.SetActive(false);
    }

    private void OnSellButtonClick()
    {
        currentShop.SellItems();
    }
}
