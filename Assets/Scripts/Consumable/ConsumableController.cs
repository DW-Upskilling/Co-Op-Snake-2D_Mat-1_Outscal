using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableController : MonoBehaviour
{
    private ConsumableType consumableType;
    private ConsumablePowerUpType consumablePowerUpType;

    public ConsumableType ConsumableType { set { consumableType = value; } get { return consumableType; } }
    public ConsumablePowerUpType ConsumablePowerUpType { set { consumablePowerUpType = value; } get { return consumablePowerUpType; } }
    public Sprite Sprite { set { SetSprite(value); } }

    void Start()
    {
        PositionHandler();
    }

    void OnDestroy()
    {
        Destroy(gameObject);
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
}
