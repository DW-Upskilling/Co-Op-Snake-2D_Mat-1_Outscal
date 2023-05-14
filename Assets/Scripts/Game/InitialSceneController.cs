using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitialSceneController : MonoBehaviour
{
    public int MenuSceneBuildIndex;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (MenuSceneBuildIndex > 0 && MenuSceneBuildIndex < SceneManager.sceneCountInBuildSettings)
                SceneManager.LoadSceneAsync(MenuSceneBuildIndex);
        }
    }
}
