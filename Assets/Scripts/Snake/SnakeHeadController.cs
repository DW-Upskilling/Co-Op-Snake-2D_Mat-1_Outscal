using System;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHeadController : MonoBehaviour
{
    public SnakeBodyHandler SnakeBodyHandler;
    public Vector3 Position
    {
        set { gameObject.GetComponent<Transform>().position = value; }
        get { return gameObject.GetComponent<Transform>().position; }
    }
    public Vector3 EulerAngles
    {
        set { gameObject.GetComponent<Transform>().eulerAngles = value; }
        get { return gameObject.GetComponent<Transform>().eulerAngles; }
    }
    public Quaternion Rotation
    {
        set { gameObject.GetComponent<Transform>().rotation = value; }
        get { return gameObject.GetComponent<Transform>().rotation; }
    }

    private int h_direction, v_direction;
    private List<ConsumablePowerUpType> consumablePowerUpType;

    void Awake()
    {
        h_direction = 1;
        v_direction = 0;
        consumablePowerUpType = new List<ConsumablePowerUpType>();
    }

    void Update()
    {
        Transform transform = gameObject.GetComponent<Transform>();
        Vector3 _position = transform.position;
        Vector3 _eulerAngles = transform.eulerAngles;

        float speed = 1f;
        if (ConsumablePowerUpTypeFind(ConsumablePowerUpType.SpeedUp) == ConsumablePowerUpType.SpeedUp)
        {
            speed = 2f;
        }

        if (h_direction != 0)
        {
            _position.x += h_direction * Time.deltaTime * speed;
            _eulerAngles = new Vector3(0, 0, h_direction == 1 ? 270 : 90);
        }
        else
        {
            _position.y += v_direction * Time.deltaTime * speed;
            _eulerAngles = new Vector3(0, 0, v_direction == 1 ? 0 : 180);
        }

        transform.position = WrapPosition(_position); ;
        transform.eulerAngles = _eulerAngles;
    }

    void OnDestroy()
    {
        Destroy(gameObject);
    }

    public void Consume(ConsumableController consumable)
    {
        if (SnakeBodyHandler == null)
            return;

        SnakeBodyHandler.Consume(consumable);
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

    public void ConsumablePowerUpTypeRemove(ConsumablePowerUpType _consumablePowerUpType)
    {
        consumablePowerUpType.Remove(_consumablePowerUpType);
    }
    public void ConsumablePowerUpTypeAdd(ConsumablePowerUpType _consumablePowerUpType)
    {
        consumablePowerUpType.Add(_consumablePowerUpType);
    }

    public ConsumablePowerUpType ConsumablePowerUpTypeFind(ConsumablePowerUpType _consumablePowerUpType)
    {
        return consumablePowerUpType.Find(e => e == _consumablePowerUpType);
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
