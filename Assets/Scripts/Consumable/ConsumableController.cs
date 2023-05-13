using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableController : MonoBehaviour
{
    public GameWorldController gameWorldController;
    public ConsumableSpawner consumableSpawner;
    public ConsumableType consumableType;

    void OnDestroy()
    {
        if (consumableSpawner != null)
            consumableSpawner.Spawn();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        // Debug.Log("OnTriggerEnter2D: " + collider.gameObject.name);
        // if (collider.gameObject.GetComponent<ConsumableController>() != null)
        // {
        //     SetRandomPosition();
        // }
        if (collider.gameObject.GetComponent<SnakeHeadController>() != null)
        {
            SnakeHeadController snakeHeadController = collider.gameObject.GetComponent<SnakeHeadController>();
            switch (consumableType)
            {
                case ConsumableType.Gainer:
                    snakeHeadController.IncrementSnakeBody(1);
                    break;
                case ConsumableType.Burner:
                    snakeHeadController.DecrementSnakeBody(1);
                    break;
            }
            Destroy(gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        // Debug.Log("OnTriggerExit2D: " + collider.gameObject.name);
        // if (collider.gameObject.GetComponent<ConsumableController>() != null)
        // {
        //     SetRandomPosition();
        // }
    }

    public void SetRandomPosition()
    {
        if (gameWorldController == null)
            return;

        // Debug.Log(
        //     "Top: " + gameWorldController.GetTopEdgePosition() +
        //     "\tBottom: " + gameWorldController.GetBottomEdgePosition() +
        //     "\tLeft: " + gameWorldController.GetLeftEdgePosition() +
        //     "\tRight: " + gameWorldController.GetRightEdgePosition()
        // );

        Vector3 position = new Vector3(
            Random.Range(gameWorldController.GetLeftEdgePosition(), gameWorldController.GetRightEdgePosition()),
            Random.Range(gameWorldController.GetBottomEdgePosition(), gameWorldController.GetTopEdgePosition()),
            -1.0f
        );
        gameObject.GetComponent<Transform>().position = position;
    }

    public void SetSprite(Sprite sprite)
    {
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
            spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;

        switch (consumableType)
        {
            case ConsumableType.Burner:
                // Red Color to the Burner Consumable
                spriteRenderer.color = Color.red;
                break;
            case ConsumableType.Gainer:
                // Green Color to the Gainer Consumable
                spriteRenderer.color = Color.green;
                break;
        }
    }
}
