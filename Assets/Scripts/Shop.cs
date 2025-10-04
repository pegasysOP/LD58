using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shop : MonoBehaviour
{
    //public List<Upgrades> upgrades;

    private Keyboard keyboard;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        keyboard = Keyboard.current;
    }

    // Update is called once per frame
    void Update()
    {
        if (keyboard.eKey.wasPressedThisFrame) OpenShop();
    }

    void OpenShop()
    {
        Debug.Log("Shopping");
    }
}
