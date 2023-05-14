using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBodyHandler : MonoBehaviour
{
    public GameObject SnakeBodyPrefab;
    public float SnakeBodySize = 1.08f;

    private List<GameObject> snakeBody;
    private List<Vector3> snakeBodyPosition;

    void OnEnable()
    {
        snakeBody = new List<GameObject>();
    }

    void OnDisable()
    {
        for (int i = snakeBody.Count - 1; i >= 0; i--)
        {
            Destroy(snakeBody[i]);
            snakeBody.RemoveAt(i);
        }

    }

    void LateUpdate()
    {
        for (int i = snakeBody.Count - 1; i >= 0; i--)
        {
            // snakeBody[i].GetComponent<SnakeBodyController>().Position = gameObject.GetComponent<Transform>().position;
            // snakeBody[i].GetComponent<SnakeBodyController>().EulerAngles = gameObject.GetComponent<Transform>().eulerAngles;
        }
    }

    void Spawn()
    {
        GameObject _snakeBody = Instantiate(SnakeBodyPrefab);

        Vector3 _position = gameObject.GetComponent<Transform>().position;
        Vector3 _eulerAngles = gameObject.GetComponent<Transform>().eulerAngles;

        if (snakeBody.Count > 0)
        {
            _position = snakeBody[snakeBody.Count - 1].GetComponent<Transform>().position;
            _eulerAngles = snakeBody[snakeBody.Count - 1].GetComponent<Transform>().eulerAngles;
        }

        _snakeBody.GetComponent<SnakeBodyController>().Position = new Vector3(
            _position.x + (_eulerAngles.z == 90 ? SnakeBodySize : 0) + (_eulerAngles.z == 270 ? -SnakeBodySize : 0),
            _position.y + (_eulerAngles.z == 180 ? SnakeBodySize : 0) + (_eulerAngles.z == 0 ? -SnakeBodySize : 0),
            _position.z
        );
        _snakeBody.GetComponent<SnakeBodyController>().EulerAngles = new Vector3(_eulerAngles.x, _eulerAngles.y, _eulerAngles.z);

        snakeBody.Add(_snakeBody);
    }

    void DeSpawn()
    {
        if (snakeBody.Count < 1)
            return;
        Destroy(snakeBody[snakeBody.Count - 1]);
        snakeBody.RemoveAt(snakeBody.Count - 1);
    }

    public void Consume(ConsumableController consumable)
    {
        switch (consumable.ConsumableType)
        {
            case ConsumableType.Burner:
                DeSpawn();
                break;
            case ConsumableType.Gainer:
                Spawn();
                break;
            case ConsumableType.PowerUp:
                Debug.Log("PowerUp");
                ActivatePowerUp(consumable);
                break;
        }
    }

    void ActivatePowerUp(ConsumableController consumable)
    {
        switch (consumable.ConsumablePowerUpType)
        {
            case ConsumablePowerUpType.Shield:
                Debug.Log("Shield");
                break;
            case ConsumablePowerUpType.ScoreBoost:
                Debug.Log("ScoreBoost");
                break;
            case ConsumablePowerUpType.SpeedUp:
                Debug.Log("SpeedUp");
                break;
        }
    }
}
