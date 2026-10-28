using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TMP_Text messageText;
    [SerializeField] private int menuSceneIndex = 0;

    private void Awake()
    {
        if (panel != null)
            panel.SetActive(false);
    }

    private void OnEnable()
    {
        // Event'e abone oluyoruz
        GameEvents.OnPlayerDied += HandlePlayerDied;
    }

    private void OnDisable()
    {
        GameEvents.OnPlayerDied -= HandlePlayerDied;
    }

    private void HandlePlayerDied()
    {
        int finalScore = ScoreManager.Instance != null ? ScoreManager.Instance.CurrentScore : 0;

        if (messageText != null)
            messageText.text = $"Congrats! You collected {finalScore} points";

        if (panel != null)
            panel.SetActive(true);
    }

    public void Restart() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    public void BackToMenu() => SceneManager.LoadScene(menuSceneIndex);
    public void Quit() => Application.Quit();
}