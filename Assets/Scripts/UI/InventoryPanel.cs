using System.Collections.Generic;
using UnityEngine;

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

    public void SetFilledSlots(int amount)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].SetFilled(i < amount);
        }
    }
}