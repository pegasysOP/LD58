using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    List<Interactable> items;
    public int maxSize = 2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        items = new List<Interactable>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool AddItem(Interactable item)
    {
        if(items.Count < maxSize)
        {
            items.Add(item);
            return true;
        }
        return false;
    }

    public bool RemoveItem(Interactable item)
    {
        if (items.Count > 0)
        {
            items.Remove(item);
            return true;
        }
        return false;
    }

    public void IncreaseInventoryCapacity(int increment = 1)
    {
        maxSize += increment;
    }

    public Interactable GetItem(int index)
    {
        return items[index];
    }

}
