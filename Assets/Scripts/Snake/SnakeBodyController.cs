using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBodyController : MonoBehaviour
{
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

    void Awake()
    {
        if (GameWorld.Instance == null)
            throw new System.Exception("You need to get a life!");
        gameObject.GetComponent<Transform>().position = new Vector3(
            GameWorld.Instance.GetRightEdgePosition() + 5,
            GameWorld.Instance.GetBottomEdgePosition() + 5,
            -1
        );
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.GetComponent<SnakeHeadController>() != null)
        {
            SnakeHeadController snakeHead = collider.gameObject.GetComponent<SnakeHeadController>();

            if (snakeHead.ConsumablePowerUpTypeFind(ConsumablePowerUpType.Shield) == ConsumablePowerUpType.Shield)
                return;

            if (snakeHead.GetComponentInParent<PlayerController>() != null)
            {
                PlayerController player = snakeHead.GetComponentInParent<PlayerController>();

                player.GameOver();
            }
        }
    }
}
