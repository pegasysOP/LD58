using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    List<ItemData> items = new List<ItemData>();
    public int maxSize = 2;
    private float money = 0f;


    public bool AddItem(ItemData item)
    {
        if(items.Count < maxSize)
        {
            items.Add(item);
            return true;
        }
        return false;
    }

    public bool RemoveItem()
    {
        if (items.Count > 0)
        {
            ItemData item = items[0];
            items.RemoveAt(0);
            money += item.Value;
            Debug.Log(money);
            return true;
        }
        return false;
    }

    public void IncreaseInventoryCapacity(int increment = 1)
    {
        maxSize += increment;
    }

    public ItemData GetItem(int index)
    {
        return items[index];
    }

    public int GetCurrentSize()
    {
        return items.Count;
    }

    public float GetMoney()
    {
        return money;
    }

    public void DeductMoney(float cost)
    {
        if(money - cost < 0f)
        {
            Debug.LogError("Trying to set negative amount.");
        }
        this.money -= cost;
    }

}
