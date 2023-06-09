using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuSceneController : MonoBehaviour
{
    public Button SinglePlayer, MultiPlayer;
    public int SinglePlayerSceneBuildIndex, MultiPlayerSceneBuildIndex;

    void Awake()
    {
        if (AudioManager.Instance)
            AudioManager.Instance.Play("Whoosh");
        if (SinglePlayer != null)
            SinglePlayer.onClick.AddListener(LoadSinglePlayerScene);
        if (MultiPlayer != null)
            MultiPlayer.onClick.AddListener(LoadMultiPlayerScene);
    }

    void LoadSinglePlayerScene()
    {
        if (AudioManager.Instance)
            AudioManager.Instance.Play("ButtonClick");
        if (SinglePlayerSceneBuildIndex > 0 && SinglePlayerSceneBuildIndex < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadSceneAsync(SinglePlayerSceneBuildIndex);
    }

    void LoadMultiPlayerScene()
    {
        if (AudioManager.Instance)
            AudioManager.Instance.Play("ButtonClick");
        if (MultiPlayerSceneBuildIndex > 0 && MultiPlayerSceneBuildIndex < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadSceneAsync(MultiPlayerSceneBuildIndex);
    }
}
