using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitialSceneController : MonoBehaviour
{
    public int MenuSceneBuildIndex;

    void Awake()
    {
        AudioManager.Instance.Play("Explosion");
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AudioManager.Instance.Play("ButtonClick");
            if (MenuSceneBuildIndex > 0 && MenuSceneBuildIndex < SceneManager.sceneCountInBuildSettings)
                SceneManager.LoadSceneAsync(MenuSceneBuildIndex);
        }
    }
}
