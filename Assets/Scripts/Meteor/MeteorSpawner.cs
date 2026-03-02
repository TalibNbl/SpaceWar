using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    [Header("Meteor Prefabs")]
    [SerializeField] private Meteor[] meteorPrefabs;

    [Header("Spawn Area")]
    [SerializeField] private float minX = -8f;
    [SerializeField] private float maxX = 8f;
    [SerializeField] private float spawnY = 6f;

    [Header("Spawn Rate")]
    [SerializeField] private float spawnInterval = 1.2f;

    private float nextSpawnTime;

    private void Update()
    {
        if (meteorPrefabs == null || meteorPrefabs.Length == 0)
            return;

        if (Time.time >= nextSpawnTime)
        {
            Spawn();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    private void Spawn()
    {
        float x = Random.Range(minX, maxX);
        Vector3 spawnPos = new Vector3(x, spawnY, 0f);

        // Pick random meteor
        int randomIndex = Random.Range(0, meteorPrefabs.Length);
        Meteor randomMeteor = meteorPrefabs[randomIndex];

        Instantiate(randomMeteor, spawnPos, Quaternion.identity);
    }
}