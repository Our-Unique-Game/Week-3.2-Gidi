using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject fruitPrefab;
    [SerializeField] private GameObject bombPrefab;
    [SerializeField] private GameObject obstaclePrefab;

    [SerializeField] private float spawnInterval = 1.5f;
    [SerializeField] private float minY = -4f, maxY = 4f; // Spawning area

    private void Start()
    {
        InvokeRepeating(nameof(SpawnObject), 0f, spawnInterval);
    }

    private void SpawnObject()
    {
        int randomType = Random.Range(0, 3); // 0 = Fruit, 1 = Bomb, 2 = Obstacle
        GameObject prefabToSpawn = null;

        switch (randomType)
        {
            case 0:
                prefabToSpawn = fruitPrefab;
                break;
            case 1:
                prefabToSpawn = bombPrefab;
                break;
            case 2:
                prefabToSpawn = obstaclePrefab;
                break;
        }

        if (prefabToSpawn != null)
        {
            Vector3 spawnPosition = new Vector3(transform.position.x, Random.Range(minY, maxY), 0);
            Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
        }
    }
}
