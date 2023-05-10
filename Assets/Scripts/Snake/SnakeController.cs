using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{

    private int h_direction, v_direction;

    void Awake()
    {
        h_direction = 1;
        v_direction = 0;
    }

    void Start() { }

    void Update()
    {
        movementController(1, 0);
    }

    void movementController(int horizontal, int vertical)
    {
        Transform transform = gameObject.GetComponent<Transform>();
        Vector3 position = transform.position;
        Vector3 eulerAngles = transform.eulerAngles;

        if (horizontal != 0)
        {
            h_direction = horizontal > 0 ? 1 : -1;
            v_direction = 0;

            position.x += h_direction * Time.deltaTime;
            eulerAngles = new Vector3(0, 0, 90 * h_direction * -1);
        }
        else if (vertical != 0)
        {
            v_direction = vertical > 0 ? 1 : -1;
            h_direction = 0;

            position.y += v_direction * Time.deltaTime;
            transform.eulerAngles = new Vector3(0, 0, v_direction == 1 ? 0 : 180);
        }

        transform.position = position;
        transform.eulerAngles = eulerAngles;
    }
}
