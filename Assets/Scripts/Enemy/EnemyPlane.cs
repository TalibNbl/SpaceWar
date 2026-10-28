using UnityEngine;

public class EnemyPlane : MonoBehaviour, IDamageable
{
    [SerializeField] private EnemyData data;
    [SerializeField] private Transform[] firePoints;
    [SerializeField] private string enemyBulletPoolTag = "EnemyBullet";

    private float targetStopY;
    private bool reachedStop;
    private float nextFireTime;
    private int currentHP;

    private void OnEnable()
    {
        if (data != null)
        {
            currentHP = data.maxHP;
            targetStopY = Random.Range(data.minStopY, data.maxStopY);
        }
        
        reachedStop = false;
        nextFireTime = 0f; // Havuzdan çıkınca zamanlayıcıyı sıfırlıyoruz
    }

    private void Update()
    {
        if (!reachedStop)
        {
            FallAndStop();
        }
        else
        {
            HandleShooting();
        }
    }

    private void FallAndStop()
    {
        transform.position += Vector3.down * (data.fallSpeed * Time.deltaTime);

        if (transform.position.y <= targetStopY)
        {
            reachedStop = true;
            transform.position = new Vector3(transform.position.x, targetStopY, transform.position.z);
            
            nextFireTime = Time.time + data.shootDelayAfterStop;
        }
    }

    private void HandleShooting()
    {
        if (data == null) return;
        if (firePoints == null || firePoints.Length == 0) return;
        if (Time.time < nextFireTime) return;

        foreach (Transform fp in firePoints)
        {
            if (fp == null) continue;

            GameObject bulletObj = ObjectPooler.Instance.SpawnFromPool(enemyBulletPoolTag, fp.position, fp.rotation);
            if (bulletObj != null && bulletObj.TryGetComponent<Bullet>(out var bullet))
            {
                bullet.Fire(Vector2.down, Faction.Enemy);
            }
        }

        nextFireTime = Time.time + (1f / data.fireRate);
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        if (currentHP <= 0) Die();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<PlayerHealth>(out var player))
        {
            player.TakeDamage(1);
            Die();
        }
    }

    private void Die()
    {
        if (data != null)
        {
            GameEvents.TriggerScoreAdded(data.scoreValue);

            if (data.explosionClip != null)
                AudioSource.PlayClipAtPoint(data.explosionClip, transform.position, data.explosionVolume);

            if (data.destroyVfxPrefab != null)
            {
                GameObject vfx = Instantiate(data.destroyVfxPrefab, transform.position, Quaternion.identity);
                vfx.transform.localScale = transform.lossyScale * data.vfxScaleMultiplier;
                Destroy(vfx, data.vfxLifeTime);
            }
        }

        gameObject.SetActive(false);
    }
}