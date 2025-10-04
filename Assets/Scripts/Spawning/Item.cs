using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject
{
    public string displayName;
    public string description;
    public GameObject itemPrefab;
    public int rarity; // 1 - common, 3 - uncommon, 5 - rare etc.
    public int valueMin;
    public int valueMax;
}
