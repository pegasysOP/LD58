using System.Runtime.CompilerServices;
using UnityEngine;

public class Interactable : MonoBehaviour, IInteractable
{
    private float rangeMultiplier = 1f;

    private int money = 1;

    public float RangeMultipler { get => rangeMultiplier; set => rangeMultiplier = value; }
    public int Value { get => money; set => money = value; }

    public void OnInteract()
    {
        Debug.LogError("Detecting is not yet implemented");
    }
}
