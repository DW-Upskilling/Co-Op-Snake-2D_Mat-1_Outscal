using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldController : MonoBehaviour
{

    public int GameOverSceneBuildIndex;
    public GameObject PauseScreen;
    public GameObject[] RemainingObjects;

    private float screenWidth, screenHeight;

    // Properties to access the screen width and height
    public float ScreenWidth { get { return screenWidth; } }
    public float ScreenHeight { get { return screenHeight; } }

    // Reference to the main camera
    public Camera mainCamera;

    // Array of player controllers
    public PlayerController[] playerControllers;

    void Awake()
    {
        // Set the position of the GameWorld to the center of the screen
        transform.position = new Vector3(0, 0, 0);

        // Calculate the half width of the screen based on the camera's orthographic size
        float screenHalfWidth = mainCamera.orthographicSize * Screen.width / Screen.height;

        // Calculate the screen width and height based on the half width and camera's orthographic size
        screenWidth = screenHalfWidth * 2;
        screenHeight = mainCamera.orthographicSize * 2;

        // Set the scale of the GameWorld to match the screen dimensions
        transform.localScale = new Vector3(screenWidth / 2, screenHeight / 2, 1f);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseScreen();
        }
    }

    public void TogglePauseScreen()
    {
        bool currentToggle = !PauseScreen.activeSelf;
        PauseScreen.SetActive(currentToggle);
        for (int i = 0; i < RemainingObjects.Length; i++)
        {
            RemainingObjects[i].SetActive(!currentToggle);
        }
    }

    public void GameOver()
    {
        if (AudioManager.Instance)
            AudioManager.Instance.Play("GameOver");

        if (gameObject.GetComponentInChildren<ConsumableSpawner>() != null)
            gameObject.GetComponentInChildren<ConsumableSpawner>().enabled = false;

        List<int> Scores = new List<int>();
        for (int i = 0; i < playerControllers.Length; i++)
        {
            Scores.Add(playerControllers[i].Score);
            Destroy(playerControllers[i]);
        }
        if (GameOverSceneBuildIndex > 0 && GameOverSceneBuildIndex < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadSceneAsync(GameOverSceneBuildIndex);
    }
}
