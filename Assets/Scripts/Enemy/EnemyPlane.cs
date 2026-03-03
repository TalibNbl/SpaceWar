using UnityEngine;

public class EnemyPlane : MonoBehaviour, IDamageable
{
    [Header("Movement")]
    [SerializeField] private float fallSpeed = 3f;

    [Header("Stop Position Range")]
    [SerializeField] private float minStopY = 1.5f;
    [SerializeField] private float maxStopY = 3f;

    [Header("Shooting")]
    [SerializeField] private Transform[] firePoints;
    [SerializeField] private Bullet enemyBulletPrefab;
    [SerializeField] private float fireRate = 1.5f;
    [SerializeField] private float shootDelayAfterStop = 0.5f;

    [Header("Sound")]
    [SerializeField] private AudioClip explosionClip;
    [SerializeField] private float explosionVolume = 1f;

    [Header("Health & Score")]
    [SerializeField] private int enemyHP = 3;
    [SerializeField] private int scoreValue = 25;

    [Header("VFX")]
    [SerializeField] private GameObject destroyVfxPrefab;
    [SerializeField] private float vfxLifeTime = 1.5f;
    [SerializeField] private float vfxScaleMultiplier = 1f;

    private float targetStopY;
    private bool reachedStop;
    private float nextFireTime;

    private void Start()
    {
        targetStopY = Random.Range(minStopY, maxStopY);
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
        transform.position += Vector3.down * fallSpeed * Time.deltaTime;

        if (transform.position.y <= targetStopY)
        {
            reachedStop = true;

            transform.position = new Vector3(transform.position.x, targetStopY, transform.position.z);

            nextFireTime = Time.time + shootDelayAfterStop;
        }
    }

    private void HandleShooting()
    {
        if (enemyBulletPrefab == null) return;
        if (firePoints == null || firePoints.Length == 0) return;

        if (Time.time < nextFireTime) return;

        foreach (Transform fp in firePoints)
        {
            if (fp == null) continue;

            Bullet b = Instantiate(enemyBulletPrefab, fp.position, fp.rotation);
            b.Fire(Vector2.down, Faction.Enemy);
        }

        nextFireTime = Time.time + (1f / fireRate);
    }

    public void TakeDamage(int damage)
    {
        enemyHP -= damage;
        if (enemyHP <= 0)
            Die();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<PlayerHealth>(out var player))
        {
            player.Die();
            Die();
        }
    }

    private void Die()
    {
        if (ScoreManager.Instance != null)
            ScoreManager.Instance.AddScore(scoreValue);

        if (explosionClip != null)
            AudioSource.PlayClipAtPoint(explosionClip, transform.position, explosionVolume);

        if (destroyVfxPrefab != null)
        {
            GameObject vfx = Instantiate(destroyVfxPrefab, transform.position, Quaternion.identity);
            vfx.transform.localScale = transform.lossyScale * vfxScaleMultiplier;
            Destroy(vfx, vfxLifeTime);
        }

        Destroy(gameObject);
    }
}