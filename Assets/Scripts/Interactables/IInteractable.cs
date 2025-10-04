using UnityEngine;

public interface IInteractable
{
    public ItemData ItemData { get; }

    public void OnInteract();
}
