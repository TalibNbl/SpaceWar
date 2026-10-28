using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [SerializeField] private TMP_Text scoreText;
    private int score;

    public int CurrentScore => score;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void OnEnable()
    {
        GameEvents.OnScoreAdded += AddScore;
    }

    private void OnDisable()
    {
        GameEvents.OnScoreAdded -= AddScore;
    }

    private void Start() => RefreshUI();

    private void AddScore(int amount)
    {
        score += amount;
        RefreshUI();
    }

    private void RefreshUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }
}