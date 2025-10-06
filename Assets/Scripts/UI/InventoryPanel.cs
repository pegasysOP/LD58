using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class InventoryPanel : MonoBehaviour
{
    public Transform slotContainer;
    public InventorySlot slotPrefab;

    private List<InventorySlot> slots = new List<InventorySlot>();

    public void SetInventorySize(int size)
    {
        foreach (InventorySlot slot in slots)
            Destroy(slot.gameObject);
        slots.Clear();


        for (int i = 0; i < size; i++)
        {
            InventorySlot newSlot = Instantiate(slotPrefab, slotContainer);
            slots.Add(newSlot);
        }
    }

    public void SetFilledSlots(List<Sprite> itemSprites)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (i >= itemSprites.Count)
                slots[i].SetFilled(null);
            else
                slots[i].SetFilled(itemSprites[i]);
        }
    }

    public void Flash()
    {
        foreach (InventorySlot slot in slots)
        {
            slot.filledIcon.DOKill();
            slot.filledIcon.color = Color.white;

            if (slot.filledIcon.gameObject.activeSelf)
                slot.filledIcon.DOColor(Color.red, 0.15f).SetLoops(2, LoopType.Yoyo);
        }
    }
}