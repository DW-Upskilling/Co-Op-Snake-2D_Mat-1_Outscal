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

        Transform transform = gameObject.GetComponent<Transform>();
        Vector3 position = transform.position;
        Vector3 eulerAngles = transform.eulerAngles;

        Vector3 _position = previous.GetComponent<Transform>().position;
        Vector3 _eulerAngles = previous.GetComponent<Transform>().eulerAngles;

        transform.position = new Vector3(
            _position.x - 1.05f,
            _position.y,
            _position.z
        );

        transform.eulerAngles = new Vector3(
            _eulerAngles.x,
            _eulerAngles.y,
            _eulerAngles.z
        );

    }
}
