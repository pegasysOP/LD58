using UnityEngine;

public interface IInteractable
{
    float rangeMultipler { get; set; }

    public void OnInteract();
}
