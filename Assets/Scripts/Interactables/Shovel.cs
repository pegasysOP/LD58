using UnityEngine;
using UnityEngine.InputSystem;

public class Shovel : MonoBehaviour
{
    float range = 2f;
    private Mouse mouse;
    public LayerMask shovelMask;
    private Inventory inventory;
     
    void Start()
    {
        mouse = Mouse.current;
        inventory = FindFirstObjectByType<Inventory>();
    }

    void Update()
    {
        if (mouse.leftButton.wasPressedThisFrame)
            Dig();
    }

    public void Dig()
    {
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, range, shovelMask);
        foreach (Collider hitCollider in hitColliders)
        {
            IInteractable interactable = hitCollider.gameObject.GetComponent<IInteractable>();
            if (interactable == null)
                continue;

            interactable.OnInteract();
            bool wasSuccessful = inventory.AddItem(interactable.ItemData);
            if (wasSuccessful)
                return;
        }
    }
}
