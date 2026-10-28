using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Transform[] firePoints;
    [SerializeField] private string bulletPoolTag = "PlayerBullet";
    [SerializeField] private float fireRate = 10f;

    private float nextFireTime;

    private void Update()
    {
        if (Time.time >= nextFireTime)
        {
            Fire();
            nextFireTime = Time.time + (1f / fireRate);
        }
    }

    private void Fire()
    {
        if (firePoints == null || firePoints.Length == 0) return;

        foreach (Transform point in firePoints)
        {
            GameObject bulletObj = ObjectPooler.Instance.SpawnFromPool(bulletPoolTag, point.position, point.rotation);
            if (bulletObj != null && bulletObj.TryGetComponent<Bullet>(out var bullet))
            {
                bullet.Fire(point.up, Faction.Player);
            }
        }
    }
}