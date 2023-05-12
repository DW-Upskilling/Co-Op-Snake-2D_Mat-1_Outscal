using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHeadController : MonoBehaviour
{

    public GameObject SnakeBodyPrefab;

    private int h_direction, v_direction;
    private SnakeBodySpawner snakeBodySpawner;
    private GameWorldController gameWorldController;

    public void SetGameWorldController(GameWorldController _gameWorldController)
    {
        gameWorldController = _gameWorldController;
    }

    void Awake()
    {
        gameObject.GetComponent<Transform>().position = new Vector3(0, 0, -1);

        h_direction = 1;
        v_direction = 0;
    }

    void Start()
    {
        snakeBodySpawner = new SnakeBodySpawner(SnakeBodyPrefab);
        // snakeBodySpawner.Spawn(gameObject);

        for (int i = 0; i < 10; i++)
        {
            // snakeBodySpawner.Spawn();
        }
    }

    void LateUpdate()
    {
        UpdatePosition();
        snakeBodySpawner.UpdatePosition();
    }

    void UpdatePosition()
    {
        Transform transform = gameObject.GetComponent<Transform>();
        Vector3 position = transform.position;
        Vector3 eulerAngles = transform.eulerAngles;

        if (h_direction != 0)
        {
            position.x += h_direction * Time.deltaTime;
            eulerAngles = new Vector3(0, 0, 90 * h_direction * -1);
            position = WrapPosition(position);
        }
        else
        {
            position.y += v_direction * Time.deltaTime;
            eulerAngles = new Vector3(0, 0, v_direction == 1 ? 0 : 180);
            position = WrapPosition(position);
        }

        transform.position = position;
        transform.eulerAngles = eulerAngles;
    }

    Vector3 WrapPosition(Vector3 position)
    {
        if (position.x < gameWorldController.GetLeftEdgePosition())
            position.x = gameWorldController.GetRightEdgePosition();
        else if (position.x > gameWorldController.GetRightEdgePosition())
            position.x = gameWorldController.GetLeftEdgePosition();
        else if (position.y < gameWorldController.GetBottomEdgePosition())
            position.y = gameWorldController.GetTopEdgePosition();
        else if (position.y > gameWorldController.GetTopEdgePosition())
            position.y = gameWorldController.GetBottomEdgePosition();

        return position;
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