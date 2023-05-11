using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBodyController : MonoBehaviour
{

    public GameObject SnakeBodyPrefab;

    private GameObject previous, next;

    public void SetParent(GameObject parent)
    {
        previous = parent;
    }
    public void SetChild(GameObject child)
    {
        next = child;
    }

    public void UpdatePosition()
    {
        if (previous.GetComponent<SnakeBodyController>() != null)
        {
            previous.GetComponent<SnakeBodyController>().UpdatePosition();
        }
    }
}
