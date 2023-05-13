using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWorldController : MonoBehaviour
{
    private float screenWidth, screenHeight;

    public Camera mainCamera;
    public PlayerController[] playerControllers;

    void Awake()
    {
        // Set position of the GameWorld to the center of screen
        transform.position = new Vector3(0, 0, 0);

        float screenHalfWidth = mainCamera.orthographicSize * Screen.width / Screen.height;

        screenWidth = screenHalfWidth * 2;
        screenHeight = mainCamera.orthographicSize * 2;

        transform.localScale = new Vector3(screenWidth / 2, screenHeight / 2, 1f);
    }

    void Start()
    {
        int totalPlayers = playerControllers.Length;
        if (totalPlayers > 1)
            for (int i = 0; i < totalPlayers; i++)
            {
                playerControllers[i].GameMode = GameMode.MultiPlayer;
            }
        else
            for (int i = 0; i < totalPlayers; i++)
            {
                playerControllers[i].GameMode = GameMode.SinglePlayer;
            }
    }

    public float GetRightEdgePosition()
    {
        return (screenWidth / 2) + 1;
    }

    public float GetLeftEdgePosition()
    {
        return (-screenWidth / 2) - 1;
    }

    public float GetTopEdgePosition()
    {
        return (screenHeight / 2) + 1;
    }

    public float GetBottomEdgePosition()
    {
        return (-screenHeight / 2) - 1;
    }

}
