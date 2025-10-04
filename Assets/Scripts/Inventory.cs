using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    List<ItemData> items = new List<ItemData>();
    public int maxSize = 2;
    private float money = 0f;
    public float maxMoney = 1000f;

    public bool AddItem(ItemData item)
    {
        if(items.Count < maxSize)
        {
            items.Add(item);
            return true;
        }
        return false;
    }

    // cursed method, do not use
    //public bool RemoveItem()
    //{
    //    if (items.Count > 0)
    //    {
    //        ItemData item = items[0];
    //        items.RemoveAt(0);
    //
    //        money += item.Value;
    //        GameManager.Instance.hudController.UpdateMoneyText(money);
    //
    //        return true;
    //    }
    //
    //    return false;
    //}

    public float SellAll()
    {
        float gains = 0f;

        foreach (ItemData item in items)
            gains += item.Value;
        items.Clear();

        money += gains;
        GameManager.Instance.hudController.UpdateMoneyText(money);

        return gains;
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
        if (money - cost < 0f)
        {
            Debug.LogWarning("Trying to set negative amount.");
            return;
        }

        this.money -= cost;

        GameManager.Instance.hudController.UpdateMoneyText(money);
    }

}
