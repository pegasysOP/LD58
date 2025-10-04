using NUnit.Framework;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shop : MonoBehaviour
{
    //public List<Upgrades> upgrades;

    private Keyboard keyboard;
    private Inventory inventory; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        keyboard = Keyboard.current;
        inventory = FindFirstObjectByType<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if (keyboard.eKey.wasPressedThisFrame) OpenShop();
    }

    void OpenShop()
    {
        for (int i = 0; i < inventory.GetCurrentSize(); i++)
        {
            inventory.RemoveItem(inventory.GetItem(i));
        }
    }
}
