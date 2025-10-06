using System.Collections.Generic;
using UnityEngine;

public class ItemCollection : MonoBehaviour
{
    public List<Item> items;
    public List<CollectionSpawnPoint> collectionSpawnPoints = new List<CollectionSpawnPoint>();

    int currentIndex = 0;

    // this is disgusting and I hate it
    public void AddItem(ItemData itemData)
    {
        if (currentIndex >= collectionSpawnPoints.Count)
        {
            Debug.LogWarning("No more spawn points available for item collection.");
            return;
        }

        // especially this part
        foreach (Item item in items)
        {
            // what the fuck
            if (item.displayName.Equals(itemData.Name))
            {
                ItemObject itemObject = Instantiate(item.objectPrefab, collectionSpawnPoints[currentIndex].spawnPoint.transform.position, Quaternion.identity);
                itemObject.col.enabled = false; // don't steal please
                itemObject.transform.parent = transform;

                items.Remove(item); // only one of each
                currentIndex++;

                break;
            }
        }
    }
}
