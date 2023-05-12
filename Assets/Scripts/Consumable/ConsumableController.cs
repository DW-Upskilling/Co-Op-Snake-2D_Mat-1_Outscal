using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableController : MonoBehaviour
{
    public GameWorldController GameWorldController;
    public ConsumableSpawner ConsumableSpawner;

    void OnDestroy()
    {
        if (ConsumableSpawner != null)
            ConsumableSpawner.Spawn();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        // Debug.Log("OnTriggerEnter2D: " + collider.gameObject.name);
        if (collider.gameObject.GetComponent<ConsumableController>() != null)
        {
            SetRandomPosition();
        }
        else if (collider.gameObject.GetComponent<SnakeHeadController>() != null)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        // Debug.Log("OnTriggerExit2D: " + collider.gameObject.name);
        if (collider.gameObject.GetComponent<ConsumableController>() != null)
        {
            SetRandomPosition();
        }
    }

    public void SetRandomPosition()
    {
        if (GameWorldController == null)
            return;

        Debug.Log(
            "Top: " + GameWorldController.GetTopEdgePosition() +
            "\tBottom: " + GameWorldController.GetBottomEdgePosition() +
            "\tLeft: " + GameWorldController.GetLeftEdgePosition() +
            "\tRight: " + GameWorldController.GetRightEdgePosition()
        );

        Vector3 position = new Vector3(
            Random.Range(GameWorldController.GetLeftEdgePosition(), GameWorldController.GetRightEdgePosition()),
            Random.Range(GameWorldController.GetBottomEdgePosition(), GameWorldController.GetTopEdgePosition()),
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
    }
}
