using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour
{
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

}
