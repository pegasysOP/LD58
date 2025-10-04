using UnityEngine;
using UnityEngine.InputSystem;

public class MetalDetector : MonoBehaviour
{
    public float range = 2f;
    float battery = 100f;
    public float maxBattery = 100f;
    public float dischargeRate = 10f;
    private Mouse mouse;

    public LayerMask metalDetectionMask;
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
        battery -= dischargeRate * Time.deltaTime;

        if (battery < 0)
        {
            return;
        }

        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, range, metalDetectionMask);
        foreach (var hitCollider in hitColliders)
        {
            Debug.Log("Battery: " + battery);
        } 
    }
}
