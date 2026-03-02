using UnityEngine;

public class Meteor : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float fallSpeed = 3.5f;
    [SerializeField] private float rotateSpeed = 90f;

    [Header("VFX")]
    [SerializeField] private GameObject destroyVfxPrefab;
    [SerializeField] private float vfxLifeTime = 1.5f;
    [SerializeField] private float vfxScaleMultiplier = 1f;

    [Header("Sound")]
    [SerializeField] private AudioClip explosionClip;
    [SerializeField] private float explosionVolume = 0.8f;

    [Header("Health")]
    [SerializeField] private int hp = 3;

    private void Update()
    {
        transform.position += Vector3.down * fallSpeed * Time.deltaTime;
        transform.Rotate(0f, 0f, rotateSpeed * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
            Die();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>()?.Die();
            Die();
        }
    }

   private void Die()
{
    if (explosionClip != null)
    {
        AudioSource.PlayClipAtPoint(explosionClip, transform.position, explosionVolume);
    }

    if (destroyVfxPrefab != null)
    {
        GameObject vfx = Instantiate(destroyVfxPrefab, transform.position, Quaternion.identity);
        vfx.transform.localScale = transform.lossyScale * vfxScaleMultiplier;
        Destroy(vfx, vfxLifeTime);
    }

    Destroy(gameObject);
}
}