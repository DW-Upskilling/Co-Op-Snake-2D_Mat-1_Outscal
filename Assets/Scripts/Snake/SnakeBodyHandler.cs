using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBodyHandler : MonoBehaviour
{
    public GameObject SnakeBodyPrefab;
    public ScoreController ScoreController;
    public float SnakeBodySize = 1.08f;

    private List<GameObject> snakeBody;
    private List<Vector3> snakeBodyPosition;
    private List<Quaternion> snakeBodyRotation;
    private int scoreMultiplier = 1;

    void OnEnable()
    {
        snakeBody = new List<GameObject>();
    }

    void Start()
    {
    }

    void OnDisable()
    {
        for (int i = snakeBody.Count - 1; i >= 0; i--)
        {
            Destroy(snakeBody[i]);
            snakeBody.RemoveAt(i);
        }

    }

    void Update()
    {
        snakeBodyPosition = new List<Vector3>();
        snakeBodyRotation = new List<Quaternion>();

        Vector3 position = gameObject.GetComponent<Transform>().position;
        Vector3 eulerAngles = gameObject.GetComponent<Transform>().eulerAngles;
        Quaternion rotation = gameObject.GetComponent<Transform>().rotation;

        for (int i = 0; i < snakeBody.Count; i++)
        {
            Vector3 _position = snakeBody[i].GetComponent<SnakeBodyController>().Position;
            Vector3 _eulerAngles = snakeBody[i].GetComponent<SnakeBodyController>().EulerAngles;
            Quaternion _rotation = snakeBody[i].GetComponent<SnakeBodyController>().Rotation;

            // Calculate the position of the next snake body segment based on the current segment's position and orientation
            Vector3 nextSegmentPosition = Vector3.zero;
            float angle = eulerAngles.z;

            // Adjust the position based on the orientation angle
            nextSegmentPosition.x = position.x + (angle == 90 ? SnakeBodySize : 0) + (angle == 270 ? -SnakeBodySize : 0);
            nextSegmentPosition.y = position.y + (angle == 180 ? SnakeBodySize : 0) + (angle == 0 ? -SnakeBodySize : 0);
            nextSegmentPosition.z = position.z;

            snakeBodyPosition.Add(nextSegmentPosition);
            snakeBodyRotation.Add(Quaternion.Lerp(_rotation, rotation, 0.5f * Time.deltaTime));

            position = _position;
            eulerAngles = _eulerAngles;
            rotation = _rotation;
        }
    }

    void LateUpdate()
    {
        for (int i = 0; i < snakeBody.Count; i++)
        {
            snakeBody[i].GetComponent<SnakeBodyController>().Position = snakeBodyPosition[i];
            snakeBody[i].GetComponent<SnakeBodyController>().Rotation = snakeBodyRotation[i];
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

        SnakeHeadController snakeHead = gameObject.GetComponent<SnakeHeadController>();
        if (snakeHead != null && snakeHead.ConsumablePowerUpTypeFind(ConsumablePowerUpType.ScoreBoost) == ConsumablePowerUpType.ScoreBoost)
        {
            scoreMultiplier = 2;
        }

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
        AudioManager.Instance.Play("Consume");
        switch (consumable.ConsumableType)
        {
            case ConsumableType.Burner:
                DeSpawn();
                break;
            case ConsumableType.Gainer:
                Spawn();
                ScoreController.Increment(1 * scoreMultiplier);
                break;
            case ConsumableType.PowerUp:
                ActivatePowerUp(consumable);
                ScoreController.Increment(10 * scoreMultiplier);
                break;
        }
    }

    void ActivatePowerUp(ConsumableController consumable)
    {
        SnakeHeadController snakeHead = gameObject.GetComponent<SnakeHeadController>();
        switch (consumable.ConsumablePowerUpType)
        {
            case ConsumablePowerUpType.Shield:
                snakeHead.ConsumablePowerUpTypeAdd(ConsumablePowerUpType.Shield);
                break;
            case ConsumablePowerUpType.ScoreBoost:
                snakeHead.ConsumablePowerUpTypeAdd(ConsumablePowerUpType.ScoreBoost);
                break;
            case ConsumablePowerUpType.SpeedUp:
                snakeHead.ConsumablePowerUpTypeAdd(ConsumablePowerUpType.SpeedUp);
                break;
        }

        StartCoroutine(DeActivatePowerUp(consumable, consumable.ConsumablePowerUpType));

    }

    IEnumerator DeActivatePowerUp(ConsumableController consumable, ConsumablePowerUpType consumablePowerUpType)
    {
        yield return new WaitForSeconds(consumable.PowerUpCoolDown);

        SnakeHeadController snakeHead = gameObject.GetComponent<SnakeHeadController>();
        snakeHead.ConsumablePowerUpTypeRemove(consumablePowerUpType);
    }
}
