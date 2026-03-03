using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [Header("Prefabs to Spawn")]
    [SerializeField] private GameObject[] prefabs;

    [Header("Spawn Area")]
    [SerializeField] private float minX = -8f;
    [SerializeField] private float maxX = 8f;
    [SerializeField] private float spawnY = 6f;

    [Header("Spawn Timing")]
    [SerializeField] private float spawnInterval = 1.2f;
    [SerializeField] private float startDelay = 0f;

    private float nextSpawnTime;

    private void Start()
    {
        nextSpawnTime = Time.time + startDelay;
    }

    private void Update()
    {
        if (prefabs == null || prefabs.Length == 0) return;

        if (Time.time >= nextSpawnTime)
        {
            Spawn();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    private void Spawn()
    {
        float x = Random.Range(minX, maxX);
        Vector3 pos = new Vector3(x, spawnY, 0f);

        int i = Random.Range(0, prefabs.Length);
        Instantiate(prefabs[i], pos, Quaternion.identity);
    }
}