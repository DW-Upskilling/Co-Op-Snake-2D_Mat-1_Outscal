using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBodyController : MonoBehaviour
{

    public GameObject SnakeBodyPrefab;

    private GameObject previousSnakeBody, nextSnakeBody;

    public void Spawn(GameObject previous)
    {
        Vector3 position = new Vector3(
            previous.transform.position.x - 1.05f,
            previous.transform.position.y,
            previous.transform.position.z
        );
        gameObject.GetComponent<Transform>().position = position;

        Vector3 eulerAngles = new Vector3(
            previous.transform.eulerAngles.x,
            previous.transform.eulerAngles.y,
            previous.transform.eulerAngles.z
        );
        gameObject.GetComponent<Transform>().eulerAngles = eulerAngles;
    }

    public void UpdatePosition(Vector3 position)
    {
        Vector3 currentPosition = new Vector3(
            position.x - 1.05f,
            position.y,
            position.z
        );
        gameObject.GetComponent<Transform>().position = currentPosition;
    }

    public void UpdateEulerAngles(Vector3 eulerAngles)
    {
        Vector3 currentEulerAngles = new Vector3(
            eulerAngles.x,
            eulerAngles.y,
            eulerAngles.z
        );
        gameObject.GetComponent<Transform>().eulerAngles = currentEulerAngles;
    }
}
