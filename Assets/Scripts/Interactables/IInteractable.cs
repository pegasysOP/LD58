using UnityEngine;

public interface IInteractable
{
    float RangeMultipler { get; set; }
    int Value { get; set; }

    public void OnInteract();
}
