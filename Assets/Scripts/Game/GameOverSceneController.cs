using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverSceneController : MonoBehaviour
{
    public Button MenuButton;
    public int MenuSceneBuildIndex;

    void Awake()
    {
        if (AudioManager.Instance)
            AudioManager.Instance.Play("Whoosh");
        if (MenuButton != null)
            MenuButton.onClick.AddListener(LoadMenuScene);
    }

    void LoadMenuScene()
    {
        if (AudioManager.Instance)
            AudioManager.Instance.Play("ButtonClick");
        if (MenuSceneBuildIndex > 0 && MenuSceneBuildIndex < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadSceneAsync(MenuSceneBuildIndex);
    }

}

