using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBodyController : MonoBehaviour
{
    public Vector3 Position { set { gameObject.GetComponent<Transform>().position = value; } get { return gameObject.GetComponent<Transform>().position; } }
    public Vector3 EulerAngles { set { gameObject.GetComponent<Transform>().eulerAngles = value; } get { return gameObject.GetComponent<Transform>().eulerAngles; } }

    void Awake()
    {
        gameObject.GetComponent<Transform>().position = new Vector3(0, 0, -1);
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.GetComponent<SnakeHeadController>() != null)
        {
            Debug.Log("Oops Touched the grass");
        }
    }
}
