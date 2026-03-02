using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 18f;
    [SerializeField] private float lifeTime = 2f;
    [SerializeField] private int damage = 1;

    private Vector2 direction = Vector2.up;

    public void Fire(Vector2 dir)
    {
        direction = dir.normalized;
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Meteor>()?.TakeDamage(damage);
            Destroy(gameObject);
        }
        
    }

    
}