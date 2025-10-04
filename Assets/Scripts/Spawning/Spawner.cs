using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Collider spawnerArea;
    public LayerMask groundLayer;
    public int maxItems = 5;

    [Header("Items")]
    public List<Item> items = new List<Item>();

    private List<ItemObject> spawnedItems = new List<ItemObject>();

    private void Start()
    {
        // initial spawn
        for (int i = 0; i < maxItems; i++)
            SpawnItem();

        InvokeRepeating(nameof(TrySpawnItem), 0f, 5f);
    }

    public void TrySpawnItem()
    {
        if (spawnedItems.Count >= maxItems)
            return;

        SpawnItem();
    }

    private void SpawnItem()
    {
        Vector3 spawnPosition = new Vector3(
        Random.Range(spawnerArea.bounds.min.x, spawnerArea.bounds.max.x),
        spawnerArea.bounds.max.y, //start at top
        Random.Range(spawnerArea.bounds.min.z, spawnerArea.bounds.max.z));

        // try and move to position of ground
        Physics.Raycast(spawnPosition, Vector3.down, out RaycastHit hitInfo, spawnerArea.bounds.max.y - spawnerArea.bounds.min.y, groundLayer);
        if (hitInfo.collider != null)
            spawnPosition.y = hitInfo.point.y;

        // TODO: go under ground in future
        spawnPosition.y -= 0.45f;

        Item chosenItem = GetItemToSpawn();
        ItemObject spawnedItem = Instantiate(chosenItem.objectPrefab, spawnPosition, Quaternion.identity);
        spawnedItem.transform.SetParent(transform);
        spawnedItem.Init(chosenItem, OnItemDestroyed);

        spawnedItems.Add(spawnedItem);
    }

    private Item GetItemToSpawn()
    {
        float totalWeight = 0f;
        foreach (Item item in items)
            totalWeight += 1f / item.rarity;

        float randomPoint = Random.Range(0f, totalWeight);

        float currentWeight = 0f;
        foreach (Item item in items)
        {
            currentWeight += 1f / item.rarity;
            if (randomPoint <= currentWeight)
                return item;
        }

        // fallback
        return items[0];
    }

    private void ResetSpawns()
    {
        foreach (ItemObject spawnedItem in spawnedItems)
            Destroy(spawnedItem);

        spawnedItems.Clear();

        for (int i = 0; i < maxItems; i++)
            SpawnItem();
    }

    private void OnItemDestroyed(ItemObject itemObject)
    {
        spawnedItems.Remove(itemObject);
    }
}
