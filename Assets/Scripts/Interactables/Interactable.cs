using UnityEngine;

public class Interactable : MonoBehaviour, IInteractable
{
    private float range = 1f;
    public float rangeMultipler { get => range; set => range = value; }

    public void OnInteract()
    {
        Debug.LogError("Detecting is not yet implemented");
    }
}
