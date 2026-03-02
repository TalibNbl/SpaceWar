using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private GameObject destroyVfxPrefab;
    [SerializeField] private AudioClip explosionClip;
    [SerializeField] private float explosionVolume = 1f;
    [SerializeField] private float vfxLifeTime = 2f;

    public void Die()
    {
        if (explosionClip != null)
        {
            AudioSource.PlayClipAtPoint(explosionClip, transform.position, explosionVolume);
        }

        if (destroyVfxPrefab != null)
        {
            GameObject vfx = Instantiate(destroyVfxPrefab, transform.position, Quaternion.identity);
            Destroy(vfx, vfxLifeTime);
        }

        Destroy(gameObject);
    }
}