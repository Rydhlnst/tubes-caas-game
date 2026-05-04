using UnityEngine;
using UnityEngine.SceneManagement;

public class InGamePauseMenuController : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject pauseMenuOverlay;

    [Header("Scene")]
    [SerializeField] private string mainMenuSceneName = "MainMenu";

    private void Start()
    {
        if (pauseMenuOverlay != null)
        {
            pauseMenuOverlay.SetActive(false);
        }

        Time.timeScale = 1f;
    }

    public void OpenPauseMenu()
    {
        if (pauseMenuOverlay != null)
        {
            pauseMenuOverlay.SetActive(true);
        }

        Time.timeScale = 0f;
    }

    public void ClosePauseMenu()
    {
        if (pauseMenuOverlay != null)
        {
            pauseMenuOverlay.SetActive(false);
        }

        Time.timeScale = 1f;
    }

    public void ResumeGame()
    {
        ClosePauseMenu();
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;

        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuSceneName);
    }
}