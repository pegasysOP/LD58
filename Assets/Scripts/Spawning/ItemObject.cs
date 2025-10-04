using UnityEngine;

public class ItemObject : MonoBehaviour, IInteractable
{
    private ItemData itemData;
    private float rangeMultiplier;

    public ItemData ItemData { get { return itemData; } }
    public float RangeMultipler { get { return rangeMultiplier; } }

    public void Init(Item itemInfo)
    {
        itemData.Name = itemInfo.displayName;
        itemData.Description = itemInfo.description;
        itemData.Value = itemInfo.GetValue();
        rangeMultiplier = itemInfo.rangeMultiplier;
    }

    public void OnInteract()
    {
        Debug.Log($"Interacted with {itemData.Name} with value: {itemData.Value}");

        Destroy(gameObject);
    }
}
