using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public Button ResumeButton, RestartButton, MainMenuButton;
    public WorldController WorldController;

    public int MenuSceneBuildIndex;

    void Awake()
    {
        AudioManager.Instance.Play("Whoosh");
        if (ResumeButton != null)
            ResumeButton.onClick.AddListener(ResumeScene);
        if (RestartButton != null)
            RestartButton.onClick.AddListener(ReloadLoadCurrentScene);
        if (MainMenuButton != null)
            MainMenuButton.onClick.AddListener(LoadMainMenuScene);
    }

    void ResumeScene()
    {
        AudioManager.Instance.Play("ButtonClick");
        if (WorldController != null)
            WorldController.TogglePauseScreen();
    }

    void ReloadLoadCurrentScene()
    {
        AudioManager.Instance.Play("ButtonClick");
        int currentSceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadSceneAsync(currentSceneBuildIndex);
    }

    void LoadMainMenuScene()
    {
        AudioManager.Instance.Play("ButtonClick");
        if (MenuSceneBuildIndex > 0 && MenuSceneBuildIndex < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadSceneAsync(MenuSceneBuildIndex);
    }
}
