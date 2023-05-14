using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBodyController : MonoBehaviour
{
    void Awake()
    {
        gameObject.GetComponent<Transform>().position = new Vector3(0, 0, -1);
    }
}
