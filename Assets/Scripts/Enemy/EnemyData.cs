using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyData", menuName = "SpaceWar/Data/Enemy Data")]
public class EnemyData : ScriptableObject
{
    [Header("Stats")]
    public int maxHP = 3;
    public float fallSpeed = 3f;
    public int scoreValue = 25;

    [Header("Movement Limits")]
    public float minStopY = 1.5f;
    public float maxStopY = 3f;

    [Header("Shooting")]
    public Bullet enemyBulletPrefab;
    public float fireRate = 1.5f;
    public float shootDelayAfterStop = 0.5f;

    [Header("VFX & Audio")]
    public GameObject destroyVfxPrefab;
    public float vfxLifeTime = 1.5f;
    public float vfxScaleMultiplier = 1f;
    public AudioClip explosionClip;
    public float explosionVolume = 1f;
}