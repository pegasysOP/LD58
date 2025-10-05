using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    public GameObject filledIcon;

    public void SetFilled(bool filled)
    {
        filledIcon.SetActive(filled);
    }
}
