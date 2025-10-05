using UnityEngine;
using UnityEngine.InputSystem;

public class Shovel : MonoBehaviour
{
    public LayerMask interactableMask;
    public LayerMask groundMask;
    public float range = 3f;

    private Mouse mouse;
     
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
        Physics.Raycast(GameManager.Instance.cameraController.playerCamera.transform.position, GameManager.Instance.cameraController.playerCamera.transform.forward, out RaycastHit hitInfo, range, interactableMask);
        IInteractable interactable= hitInfo.collider?.GetComponent<IInteractable>();
        if (interactable == null)
        {
            Physics.Raycast(GameManager.Instance.cameraController.playerCamera.transform.position, GameManager.Instance.cameraController.playerCamera.transform.forward, out hitInfo, range, groundMask);
            if (hitInfo.collider == null)
                return;

            // TODO: put sand particles here if we want them
            AudioManager.Instance.PlaySfxWithPitchShifting(AudioManager.Instance.digSandClips);
            return;
        }

        interactable.OnInteract();
        return;

        //Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, range, shovelMask);
        //foreach (Collider hitCollider in hitColliders)
        //{
        //    IInteractable interactable = hitCollider.gameObject.GetComponent<IInteractable>();
        //    if (interactable == null)
        //        continue;
        //
        //    interactable.OnInteract();
        //    return;
        //}
    }

    private void OnDrawGizmos()
    {
        if (GameManager.Instance == null || GameManager.Instance.cameraController == null || GameManager.Instance.cameraController.playerCamera == null)
            return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(GameManager.Instance.cameraController.playerCamera.transform.position, GameManager.Instance.cameraController.playerCamera.transform.position + GameManager.Instance.cameraController.playerCamera.transform.forward * range);
    }
}
