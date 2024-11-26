using UnityEngine;

public class LaneSpawner : MonoBehaviour
{
    [Tooltip("Items that can spawn (e.g., fruits, bombs, obstacles)")]
    [SerializeField] private GameObject[] spawnableObjects;

    [Tooltip("Maximum number of items to spawn within the range")]
    [SerializeField] private int maxItems = 10;

    [Tooltip("Maximum distance for the entire spawn range")]
    [SerializeField] private float maxSpawnRange = 1000f;

    [Tooltip("Maximum distance between consecutive items")]
    [SerializeField] private float maxDistanceBetweenItems = 10f;

    [Tooltip("Parent object for spawned items (optional)")]
    [SerializeField] private Transform parentObject;

    private float spawnStartPositionX;

    private void Start()
    {
        spawnStartPositionX = transform.position.x;

        // Spawn the items in the lane
        SpawnItems();
    }

    private void SpawnItems()
    {
        float currentXPosition = spawnStartPositionX;

        for (int i = 0; i < maxItems; i++)
        {
            // Randomly pick an object to spawn
            int randomIndex = Random.Range(0, spawnableObjects.Length);
            GameObject selectedObject = spawnableObjects[randomIndex];

            // Calculate random distance between items
            float randomDistance = Random.Range(5f, maxDistanceBetweenItems); // Adjust the minimum distance as needed

            // Update the current spawn position
            currentXPosition += randomDistance;

            // Ensure we don't exceed the max spawn range
            if (currentXPosition - spawnStartPositionX > maxSpawnRange)
            {
                break; // Stop spawning if we exceed the range
            }

            // Spawn the object at the calculated position
            Vector3 spawnPosition = new Vector3(currentXPosition, transform.position.y, 0);
            Instantiate(selectedObject, spawnPosition, Quaternion.identity, parentObject);
        }
    }
}
