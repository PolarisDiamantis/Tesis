using UnityEngine;
using System.Collections.Generic;

public class TornadoLevel4 : MonoBehaviour
{
    [SerializeField] private List<GameObject> objectsToSpawn;
    [SerializeField] private Vector3 spawnAreaSize;
    [SerializeField] private float spawnInterval = 5f;

    void Start()
    {
        InvokeRepeating("SpawnObject", 0f, spawnInterval);
    }

    public void SpawnObject()
    {
        if (objectsToSpawn != null && objectsToSpawn.Count > 0)
        {
            Vector3 spawnPosition = GetRandomPositionInArea(transform.position, spawnAreaSize);
            GameObject objectToSpawn = objectsToSpawn[Random.Range(0, objectsToSpawn.Count)];
            Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
        }
    }

    private Vector3 GetRandomPositionInArea(Vector3 center, Vector3 size)
    {
        float randomX = Random.Range(center.x - size.x / 2, center.x + size.x / 2);
        float randomY = Random.Range(center.y - size.y / 2, center.y + size.y / 2);
        float randomZ = Random.Range(center.z - size.z / 2, center.z + size.z / 2);

        return new Vector3(randomX, randomY, randomZ);
    }
}