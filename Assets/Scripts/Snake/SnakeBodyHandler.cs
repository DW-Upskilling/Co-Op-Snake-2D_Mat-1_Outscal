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
    private Vector3 snakeHeadPosition;
    private List<Quaternion> snakeBodyRotation;
    private Quaternion snakeHeadRotation;
    private int scoreMultiplier = 1;
    private float maxTime, currentSegmentTime;

    void Awake()
    {
        snakeBody = new List<GameObject>();
        maxTime = 1f;
        currentSegmentTime = 0f;
    }

    void Start()
    {
        snakeHeadPosition = gameObject.GetComponent<Transform>().position;
        snakeHeadRotation = gameObject.GetComponent<Transform>().rotation;
    }

    void Update()
    {
        currentSegmentTime += Time.deltaTime;

        snakeBodyPosition = new List<Vector3>();
        snakeBodyRotation = new List<Quaternion>();

        SnakeBodyController currentBody;
        Vector3 currentPosition, previousPosition;
        Quaternion currentRotation, previousRotation;
        Vector3 currentEulerAngles, previousEulerAngles;

        previousPosition = snakeHeadPosition;
        previousEulerAngles = gameObject.GetComponent<Transform>().eulerAngles;
        previousRotation = snakeHeadRotation;

        for (int i = 0; i < snakeBody.Count; i++)
        {
            currentBody = snakeBody[i].GetComponent<SnakeBodyController>();
            currentPosition = currentBody.Position;
            currentEulerAngles = currentBody.EulerAngles;
            currentRotation = currentBody.Rotation;

            Vector3 nextPosition = new Vector3(
                previousPosition.x + (previousEulerAngles.z == 90 ? SnakeBodySize : 0) + (previousEulerAngles.z == 270 ? -SnakeBodySize : 0),
                previousPosition.y + (previousEulerAngles.z == 180 ? SnakeBodySize : 0) + (previousEulerAngles.z == 0 ? -SnakeBodySize : 0),
                previousPosition.z
            );
            nextPosition = currentPosition + (nextPosition - currentPosition) * Time.deltaTime;

            Quaternion nextRotation = previousRotation;

            if (currentRotation.z != previousRotation.z)
            {
                nextRotation = Quaternion.Lerp(previousRotation, currentRotation, 1f * Time.deltaTime);
            }
            else if (currentSegmentTime < maxTime || previousRotation.z % 90 != 0)
            {
                nextRotation = currentRotation;
            }

            snakeBodyPosition.Add(nextPosition);
            snakeBodyRotation.Add(nextRotation);

            previousPosition = currentPosition;
            previousRotation = currentRotation;
            previousEulerAngles = currentEulerAngles;
        }

        if (currentSegmentTime >= maxTime)
        {
            currentSegmentTime -= maxTime;
        }
    }

    void LateUpdate()
    {
        for (int i = 0; i < snakeBody.Count && i < snakeBodyPosition.Count && i < snakeBodyRotation.Count; i++)
        {
            snakeBody[i].GetComponent<SnakeBodyController>().Position = snakeBodyPosition[i];
            snakeBody[i].GetComponent<SnakeBodyController>().Rotation = snakeBodyRotation[i];
        }

        snakeHeadPosition = gameObject.GetComponent<Transform>().position;
        snakeHeadRotation = gameObject.GetComponent<Transform>().rotation;
    }

    void OnDestroy()
    {
        for (int i = snakeBody.Count - 1; i >= 0; i--)
        {
            Destroy(snakeBody[i]);
            snakeBody.RemoveAt(i);
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
        if (AudioManager.Instance)
            AudioManager.Instance.Play("Consume");
        switch (consumable.ConsumableType)
        {
            case ConsumableType.Burner:
                // Decreasing score didn't made sense
                // Only decreasing the length of the body
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
