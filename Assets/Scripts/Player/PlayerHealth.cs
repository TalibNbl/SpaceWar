using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private GameObject destroyVfxPrefab;
    [SerializeField] private AudioClip explosionClip;
    [SerializeField] private float explosionVolume = 1f;
    [SerializeField] private float vfxLifeTime = 2f;
    [SerializeField] private GameOverUI gameOverUI;
    
    private void Awake()
    {
        gameOverUI.GetComponent<GameOverUI>();
    }

    public void TakeDamage(int damage) => Die();
    
    public void Die()
    {
        int finalScore = 0;
        if (ScoreManager.Instance != null)
            finalScore = ScoreManager.Instance.CurrentScore;

        if (gameOverUI != null)
            gameOverUI.Show(finalScore);
        else
            Debug.LogError("GameOverUI reference not assigned on PlayerHealth!");

        gameObject.SetActive(false);
    }

    
}