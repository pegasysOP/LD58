using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image filledIcon;

    public void SetFilled(Sprite itemSprite)
    {
        filledIcon.gameObject.SetActive(itemSprite != null);
        if (itemSprite != null)
            filledIcon.sprite = itemSprite;
    }
}
