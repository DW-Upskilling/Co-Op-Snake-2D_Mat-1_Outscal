using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverSceneController : MonoBehaviour
{
    public Button MenuButton;
    public int MenuSceneBuildIndex;

    void Awake()
    {
        if (MenuButton != null)
            MenuButton.onClick.AddListener(LoadMenuScene);
    }

    void LoadMenuScene()
    {
        if (MenuSceneBuildIndex > 0 && MenuSceneBuildIndex < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadSceneAsync(MenuSceneBuildIndex);
    }

}

