using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [Header("Pool Tags to Spawn")]
    [SerializeField] private string[] enemyPoolTags; // Örn: "EnemyPlane", "Meteor"

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
        if (enemyPoolTags == null || enemyPoolTags.Length == 0) return;

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

        // Rastgele bir düşman tag'i seçip havuzdan doğuruyoruz
        string randomTag = enemyPoolTags[Random.Range(0, enemyPoolTags.Length)];
        ObjectPooler.Instance.SpawnFromPool(randomTag, pos, Quaternion.identity);
    }
}