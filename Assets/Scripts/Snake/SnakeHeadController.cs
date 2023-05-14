using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHeadController : MonoBehaviour
{
    public SnakeBodySpawner SnakeBodySpawner;

    private int h_direction, v_direction;
    private Vector3 position, eulerAngles;

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
        Transform transform = gameObject.GetComponent<Transform>();
        Vector3 _position = transform.position;
        Vector3 _eulerAngles = transform.eulerAngles;

        if (h_direction != 0)
        {
            _position.x += h_direction * Time.deltaTime;
            _eulerAngles = new Vector3(0, 0, h_direction == 1 ? 270 : 90);
        }
        else
        {
            _position.y += v_direction * Time.deltaTime;
            _eulerAngles = new Vector3(0, 0, v_direction == 1 ? 0 : 180);
        }

        position = WrapPosition(_position);
        eulerAngles = _eulerAngles;
    }

    void LateUpdate()
    {
        Transform transform = gameObject.GetComponent<Transform>();
        transform.position = position;
        transform.eulerAngles = eulerAngles;
    }

    void OnDestroy()
    {
        Destroy(gameObject);
    }

    public void Consume(ConsumableController consumable)
    {
        if (SnakeBodySpawner == null)
            return;

        SnakeBodySpawner.Consume(consumable);
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
