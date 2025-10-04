using UnityEngine;
using UnityEngine.InputSystem;

public class Shovel : MonoBehaviour
{
    float range = 2f;
    private Mouse mouse;
    public LayerMask shovelMask;
    private Inventory inventory;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mouse = Mouse.current;
        inventory = FindFirstObjectByType<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mouse.leftButton.wasPressedThisFrame) Dig();
    }

    public void Dig()
    {
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, range, shovelMask);
        foreach (var hitCollider in hitColliders)
        {
            bool wasSuccessful = inventory.AddItem(hitCollider.gameObject.GetComponent<Interactable>());
        }
    }
}
