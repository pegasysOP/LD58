using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUpgrade : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI costText;
    public Button upgradeButton;

    private void OnEnable()
    {
        upgradeButton.onClick.AddListener(OnUpgradeButtonClick);
    }
    
    private void OnDisable()
    {
        upgradeButton.onClick.RemoveListener(OnUpgradeButtonClick);
    }

    public void Init(string name, float cost)
    {
        nameText.text = name;
        costText.text = "$" + cost.ToString("F2");
    }

    private void OnUpgradeButtonClick()
    {
        Debug.Log("Buy upgrade button clicked");
    }
}
