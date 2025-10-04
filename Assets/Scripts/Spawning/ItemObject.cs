using System;
using UnityEngine;

public class ItemObject : MonoBehaviour, IInteractable
{
    private ItemData itemData;
    private float rangeMultiplier;
    private Action<ItemObject> onDestroy;

    public ItemData ItemData { get { return itemData; } }
    public float RangeMultipler { get { return rangeMultiplier; } }

    public void Init(Item itemInfo, Action<ItemObject> onDestroy)
    {
        this.itemData.Name = itemInfo.displayName;
        this.itemData.Description = itemInfo.description;
        this.itemData.Value = itemInfo.GetValue();
        this.rangeMultiplier = itemInfo.rangeMultiplier;

        this.onDestroy = onDestroy;
    }

    public void OnInteract()
    {
        Debug.Log($"Interacted with {itemData.Name} with value: {itemData.Value}");

        onDestroy?.Invoke(this);

        Destroy(gameObject);
    }
}
