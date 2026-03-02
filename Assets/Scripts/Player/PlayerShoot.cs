using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Transform[] firePoints;
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Animator playerAnim;

    [Header("Muzzle FX")]
    [SerializeField] private ParticleSystem leftGunFx;
    [SerializeField] private ParticleSystem rightGunFx;
    [SerializeField] private ParticleSystem centralGunFx;

    [Header("Sound")]
    [SerializeField] private AudioSource shootAudioSource;
    [SerializeField] private AudioClip shootClip;

    [Header("Fire Settings")]
    [SerializeField] private float fireRate = 10f;

    private float nextFireTime;

    private void Update()
    {
        if (Time.time >= nextFireTime)
        {
            FireFX();
            nextFireTime = Time.time + (1f / fireRate);
        }
    }

   private void FireFX()
    {
        if (firePoints == null || firePoints.Length == 0) return;

        foreach (Transform point in firePoints)
        {
            Bullet newBullet = Instantiate(bulletPrefab, point.position, point.rotation);
            newBullet.Fire(point.up);
        }

        if (playerAnim != null)
            playerAnim.SetTrigger("Shoot");

        PlayFx(leftGunFx);
        PlayFx(rightGunFx);
        PlayFx(centralGunFx);

        // 🔊 Play sound
        if (shootAudioSource != null && shootClip != null)
            shootAudioSource.PlayOneShot(shootClip);
    }

    private void PlayFx(ParticleSystem fx)
    {
        if (fx == null) return;

        fx.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        fx.Play();
    }
}
