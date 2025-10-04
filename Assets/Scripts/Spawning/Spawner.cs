using UnityEngine;
using UnityEngine.InputSystem;

public class Spawner : MonoBehaviour
{
    public Collider spawnerArea;
    public GameObject placeholderItem;
    public LayerMask groundLayer;

    private void Update()
    {
        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            SpawnItem();
        }
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

        Instantiate(placeholderItem, spawnPosition, Quaternion.identity);
    }
}
