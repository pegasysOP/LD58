using UnityEngine;
using UnityEngine.InputSystem;

public class Shovel : MonoBehaviour
{
    float range = 2f;
    private Mouse mouse;
    public LayerMask shovelMask;
     
    void Start()
    {
        mouse = Mouse.current;
    }

    void Update()
    {
        if (GameManager.Instance.LOCKED)
            return;

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
            return;
        }
    }
}
