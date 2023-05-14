using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBodySpawner : MonoBehaviour
{

    public void Consume(ConsumableController consumable)
    {
        switch (consumable.ConsumableType)
        {
            case ConsumableType.Burner:
                Debug.Log("Burner");
                break;
            case ConsumableType.Gainer:
                Debug.Log("Gainer");
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
