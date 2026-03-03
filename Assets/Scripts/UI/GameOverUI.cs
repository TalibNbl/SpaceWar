using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TMP_Text messageText;

    [Header("Scene Indexes")]
    [SerializeField] private int menuSceneIndex = 0;

    private void Awake()
    {
        if (panel != null)
            panel.SetActive(false);
    }

    public void Show(int score)
    {
        if (messageText != null)
            messageText.text = $"Congrats! You collected {score} points";

        if (panel != null)
            panel.SetActive(true);

    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(menuSceneIndex);
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}