using UnityEngine;
using UnityEngine.InputSystem;

public class MetalDetector : MonoBehaviour
{
    float range = 2f;
    float battery = 100f;
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
        //TODO: Implement detecting. Find all objects with IInteractable and check their range
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, range, metalDetectionMask);
        foreach (var hitCollider in hitColliders)
        {
            Debug.Log("Hit " + hitCollider.name);
        } 

    }
}
