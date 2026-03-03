using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifeTime = 3f;
    [SerializeField] private int damage = 1;

    private Vector2 direction;
    private Faction ownerFaction;

    public void Fire(Vector2 dir, Faction owner)
    {
        direction = dir.normalized;
        ownerFaction = owner;

        CancelInvoke();
        Invoke(nameof(DestroyBullet), lifeTime);
    }

    private void Update()
    {
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var damageable = other.GetComponentInParent<IDamageable>();
        if (damageable == null) return;

        var factionMember = other.GetComponentInParent<FactionMember>();
        if (factionMember == null) return;

        if (factionMember.faction == ownerFaction) return;

        damageable.TakeDamage(damage);
        DestroyBullet();
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
}