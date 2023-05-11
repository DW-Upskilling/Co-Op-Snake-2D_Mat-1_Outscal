using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWorldController : MonoBehaviour
{

    public Camera mainCamera;
    public float objectLength;
    void Awake()
    {
        // Set position of the GameWorld to the center of screen
        transform.position = new Vector3(0, 0, 0);

        Vector3 cameraPosition = mainCamera.transform.position;
        float cameraDistance = Vector3.Distance(cameraPosition, transform.position);

        float objectHeight = 2f * Mathf.Tan(mainCamera.fieldOfView * 0.5f * Mathf.Deg2Rad) * cameraDistance;
        float objectWidth = objectHeight * mainCamera.aspect;

        transform.localScale = new Vector3(objectWidth, objectHeight, 1f);

    }

    public float GetRightEdgePosition()
    {
        return 15f;
    }

    public float GetLeftEdgePosition()
    {
        return -15f;
    }

    public float GetTopEdgePosition()
    {
        return 7.5f;
    }

    public float GetBottomEdgePosition()
    {
        return -7.5f;
    }

}
