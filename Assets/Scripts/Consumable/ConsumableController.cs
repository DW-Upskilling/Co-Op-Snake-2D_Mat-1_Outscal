using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableController : MonoBehaviour
{
    public ConsumableType ConsumableType
    {
        set { consumableType = value; }
        get { return consumableType; }
    }
    public ConsumablePowerUpType ConsumablePowerUpType
    {
        set { consumablePowerUpType = value; }
        get { return consumablePowerUpType; }
    }
    public float PowerUpCoolDown
    {
        set { powerUpCoolDown = value; }
        get { return powerUpCoolDown; }
    }
    public Sprite Sprite { set { SetSprite(value); } }

    private ConsumableType consumableType;
    private ConsumablePowerUpType consumablePowerUpType;
    private float powerUpCoolDown = 3;

    void Start()
    {
        PositionHandler();
    }

    void OnDestroy()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.GetComponent<ConsumableController>() != null)
        {
            PositionHandler();
        }
        if (collider.gameObject.GetComponent<SnakeHeadController>() != null)
        {
            SnakeHeadController snakeHead = collider.gameObject.GetComponent<SnakeHeadController>();

            snakeHead.Consume(gameObject.GetComponent<ConsumableController>());
            Destroy(gameObject);
        }
    }

    void SetSprite(Sprite sprite)
    {
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
            spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;

        switch (consumableType)
        {
            case ConsumableType.Burner:
                spriteRenderer.color = Color.red;
                break;
            case ConsumableType.Gainer:
                spriteRenderer.color = Color.green;
                break;
            case ConsumableType.PowerUp:
                spriteRenderer.color = Color.blue;
                break;
        }
    }

    void PositionHandler()
    {
        if (GameWorld.Instance == null)
            return;

        GameWorld gameWorld = GameWorld.Instance;

        Vector3 position = new Vector3(
            Random.Range(gameWorld.GetLeftEdgePosition(), gameWorld.GetRightEdgePosition()),
            Random.Range(gameWorld.GetBottomEdgePosition(), gameWorld.GetTopEdgePosition()),
            -1.0f
        );
        gameObject.GetComponent<Transform>().position = position;
    }

    public void LifeTime(float remainingOpacity, float remainingScale)
    {
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
            spriteRenderer = gameObject.AddComponent<SpriteRenderer>();

        Color spriteColor = spriteRenderer.color;
        Vector3 initialScale = new Vector3(1, 1, 1);

        // Update opacity
        spriteColor.a = remainingOpacity;
        spriteRenderer.color = spriteColor;

        // Update scale
        Vector3 newScale = initialScale * remainingScale;
        transform.localScale = newScale;
    }
}
