using UnityEngine;
using UnityEngine.InputSystem;

public class Shovel : MonoBehaviour
{
    private Mouse mouse;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mouse = Mouse.current;
    }

    // Update is called once per frame
    void Update()
    {
        if (mouse.leftButton.isPressed) Dig();
    }

    public void Dig()
    {
        Debug.Log("Digging! Diggy diggy hole!");
    }
}
