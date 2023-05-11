using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHeadController : MonoBehaviour
{

    public GameObject SnakeBodyPrefab;

    private int h_direction, v_direction;
    private GameObject tail;

    void Awake()
    {
        gameObject.GetComponent<Transform>().position = new Vector3(0, 0, 0);

        h_direction = 1;
        v_direction = 0;
    }

    void Start()
    {

        tail = Instantiate(SnakeBodyPrefab);
        tail.GetComponent<SnakeBodyController>().Spawn(gameObject);

    }

    void LateUpdate()
    {
        updateSnakePosition();
    }

    void updateSnakePosition()
    {

        Transform transform = gameObject.GetComponent<Transform>();
        Vector3 position = transform.position;
        Vector3 eulerAngles = transform.eulerAngles;

        if (h_direction != 0)
        {
            position.x += h_direction * Time.deltaTime;
            eulerAngles = new Vector3(0, 0, 90 * h_direction * -1);

        }
        else
        {
            position.y += v_direction * Time.deltaTime;
            eulerAngles = new Vector3(0, 0, v_direction == 1 ? 0 : 180);
        }

        transform.position = position;
        transform.eulerAngles = eulerAngles;

    }

    public void PositionHandler(float horizontal, float vertical)
    {
        if (horizontal != 0)
        {
            int _h_direction = horizontal > 0 ? 1 : -1;
            if (h_direction == _h_direction * -1)
                return;

            h_direction = _h_direction;
            v_direction = 0;
        }
        else if (vertical != 0)
        {
            int _v_direction = vertical > 0 ? 1 : -1;
            if (v_direction == _v_direction * -1)
                return;

            v_direction = _v_direction;
            h_direction = 0;
        }
    }
}
