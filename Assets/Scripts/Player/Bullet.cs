using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifeTime = 3f;
    [SerializeField] private int damage = 1;

    private Vector2 direction;
    private Faction ownerFaction;
    private float deactivateTime;

    public void Fire(Vector2 dir, Faction owner)
    {
        direction = dir.normalized;
        ownerFaction = owner;
        deactivateTime = Time.time + lifeTime;
    }

    private void Update()
    {
        transform.position += (Vector3)(direction * speed * Time.deltaTime);

        if (Time.time >= deactivateTime)
        {
            gameObject.SetActive(false); 
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<FactionMember>(out var factionMember))
        {
            if (factionMember.faction == ownerFaction) return;
        }

        if (other.TryGetComponent<IDamageable>(out var damageable))
        {
            damageable.TakeDamage(damage);
            gameObject.SetActive(false);
        }
    }
}