using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWorld : MonoBehaviour
{

    // Singleton instance of GameWorld
    private static GameWorld instance;
    public static GameWorld Instance { get { return instance; } }

    // Reference to the WorldController script
    public WorldController WorldController;

    private float screenWidth, screenHeight;

    void Awake()
    {
        // Singleton pattern implementation
        // DontDestroyOnLoad: Not Required
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Retrieve screen width and height from WorldController script
        screenWidth = WorldController.ScreenWidth;
        screenHeight = WorldController.ScreenHeight;
    }

    // Returns the right edge position of the screen
    public float GetRightEdgePosition()
    {
        return screenWidth / 2;
    }

    // Returns the left edge position of the screen
    public float GetLeftEdgePosition()
    {
        return -screenWidth / 2;
    }

    // Returns the top edge position of the screen
    public float GetTopEdgePosition()
    {
        return screenHeight / 2;
    }

    // Returns the bottom edge position of the screen
    public float GetBottomEdgePosition()
    {
        return -screenHeight / 2;
    }
}
