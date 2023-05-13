using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHeadController : MonoBehaviour
{
    public SnakeBodySpawner SnakeBodySpawner;

    private int h_direction, v_direction;

    void Awake()
    {
        h_direction = 1;
        v_direction = 0;
    }

    void OnEnable()
    {
        gameObject.GetComponent<Transform>().position = new Vector3(0, 0, -1);
    }

    void Update()
    {
        Debug.Log(h_direction + "\t" + v_direction);

        Transform transform = gameObject.GetComponent<Transform>();
        Vector3 position = transform.position;
        Vector3 eulerAngles = transform.eulerAngles;

        if (h_direction != 0)
        {
            position.x += h_direction * Time.deltaTime;
            eulerAngles = new Vector3(0, 0, h_direction == 1 ? 270 : 90);
        }
        else
        {
            position.y += v_direction * Time.deltaTime;
            eulerAngles = new Vector3(0, 0, v_direction == 1 ? 0 : 180);

        }

        position = WrapPosition(position);
        transform.position = position;
        transform.eulerAngles = eulerAngles;
    }

    void OnDestroy()
    {
        Destroy(gameObject);
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

    Vector3 WrapPosition(Vector3 position)
    {
        if (GameWorld.Instance == null)
            return position;

        GameWorld gameWorld = GameWorld.Instance;

        if (position.x < gameWorld.GetLeftEdgePosition())
            position.x = gameWorld.GetRightEdgePosition();
        else if (position.x > gameWorld.GetRightEdgePosition())
            position.x = gameWorld.GetLeftEdgePosition();
        else if (position.y < gameWorld.GetBottomEdgePosition())
            position.y = gameWorld.GetTopEdgePosition();
        else if (position.y > gameWorld.GetTopEdgePosition())
            position.y = gameWorld.GetBottomEdgePosition();

        return position;
    }
}
