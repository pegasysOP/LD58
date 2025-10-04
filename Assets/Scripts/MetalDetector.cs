using UnityEngine;
using UnityEngine.InputSystem;

public class MetalDetector : MonoBehaviour
{
    float range = 2f;
    float battery = 100f;
    private Keyboard keyboard;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        keyboard = Keyboard.current;
    }

    // Update is called once per frame
    void Update()
    {
        if (keyboard.eKey.isPressed) Detect();
    }

    public void Detect()
    {
        Debug.Log("Detecting! Beep Beep Beep!");
    }
}
