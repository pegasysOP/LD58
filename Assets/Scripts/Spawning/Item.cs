using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject
{
    public string displayName;
    public string description;
    public ItemObject objectPrefab;
    public Sprite icon;
    public int rarity = 1; // 1 - common, 3 - uncommon, 5 - rare etc.
    public float rangeMultiplier = 1f;
    public int valueMin = 1;
    public int valueMax = 1;
    public bool isSilver;
    public bool isGold;

    public int GetValue()
    {
        return Random.Range(valueMin, valueMax + 1);
    }
}
