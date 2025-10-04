using UnityEngine;
using UnityEngine.InputSystem;

public class MetalDetector : MonoBehaviour
{
    float range = 2f;
    float battery = 100f;
    private Mouse mouse; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mouse = Mouse.current;
    }

    // Update is called once per frame
    void Update()
    {
        if (mouse.rightButton.isPressed) Detect();
    }

    public void Detect()
    {
        Debug.Log("Detecting! Beep Beep Beep!");
    }
}
