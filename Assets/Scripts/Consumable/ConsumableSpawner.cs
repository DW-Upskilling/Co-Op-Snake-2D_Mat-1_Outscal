using System;
using System.Collections;
using UnityEngine;

public class ConsumableSpawner : MonoBehaviour
{
    public GameObject ConsumablePrefab;

    public Sprite[] GainerSprite, BurnerSprite;
    public Sprite ShieldSprite, ScoreBoostSprite, SpeedUpSprite;

    public float powerUpCoolDown = 3;
    public float SpawnIntervalFood = 10, SpawnIntervalPowerUpMin = 10, SpawnIntervalPowerUpMax = 60;
    public int MaxConsumables = 20;

    private int consumablePool = 0;
    private object lockObject = new object();

    void OnEnable()
    {
        StartCoroutine(SpawnFoodCoroutine());
        StartCoroutine(SpawnPowerUpCoroutine());
    }

    void OnDisable()
    {
        StopCoroutine(SpawnFoodCoroutine());
        StopCoroutine(SpawnPowerUpCoroutine());
    }

    ConsumableController SpawnFood()
    {
        Array ConsumableTypes = Enum.GetValues(typeof(ConsumableType));
        int randomIndex = UnityEngine.Random.Range(0, ConsumableTypes.Length);

        ConsumableType consumableType = (ConsumableType)ConsumableTypes.GetValue(randomIndex);

        ConsumableController consumable = Instantiate(ConsumablePrefab).GetComponent<ConsumableController>();

        switch (consumableType)
        {
            case ConsumableType.Burner:
                consumable.ConsumableType = ConsumableType.Burner;
                consumable.Sprite = BurnerSprite[UnityEngine.Random.Range(0, BurnerSprite.Length)];
                break;
            case ConsumableType.Gainer:
                consumable.ConsumableType = ConsumableType.Gainer;
                consumable.Sprite = GainerSprite[UnityEngine.Random.Range(0, GainerSprite.Length)];
                break;
            case ConsumableType.PowerUp:
                Destroy(consumable);
                consumable = SpawnFood();
                break;
        }

        return consumable;
    }

    ConsumableController SpawnPowerUp()
    {
        Array ConsumablePowerUpTypes = Enum.GetValues(typeof(ConsumablePowerUpType));
        int randomIndex = UnityEngine.Random.Range(0, ConsumablePowerUpTypes.Length);

        ConsumablePowerUpType consumablePowerUpType = (ConsumablePowerUpType)ConsumablePowerUpTypes.GetValue(randomIndex);

        ConsumableController consumable = Instantiate(ConsumablePrefab).GetComponent<ConsumableController>();
        consumable.ConsumableType = ConsumableType.PowerUp;

        switch (consumablePowerUpType)
        {
            case ConsumablePowerUpType.Shield:
                consumable.ConsumablePowerUpType = ConsumablePowerUpType.Shield;
                consumable.Sprite = ShieldSprite;
                break;
            case ConsumablePowerUpType.ScoreBoost:
                consumable.ConsumablePowerUpType = ConsumablePowerUpType.ScoreBoost;
                consumable.Sprite = ScoreBoostSprite;
                break;
            case ConsumablePowerUpType.SpeedUp:
                consumable.ConsumablePowerUpType = ConsumablePowerUpType.SpeedUp;
                consumable.Sprite = SpeedUpSprite;
                break;
        }

        return consumable;
    }

    IEnumerator SpawnFoodCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(SpawnIntervalFood);

            if (consumablePool >= MaxConsumables)
                continue;

            lock (lockObject)
            {
                ConsumableController consumable = SpawnFood();
                consumablePool++;

                StartCoroutine(DeSpawnCoroutine(consumable, SpawnIntervalFood * 10f));
            }

        }
    }

    IEnumerator SpawnPowerUpCoroutine()
    {
        while (true)
        {
            float currentInterval = UnityEngine.Random.Range(SpawnIntervalPowerUpMin, SpawnIntervalPowerUpMax + 1f);
            yield return new WaitForSeconds(currentInterval);

            if (consumablePool >= MaxConsumables)
                continue;

            lock (lockObject)
            {
                ConsumableController consumable = SpawnPowerUp();
                consumablePool++;

                StartCoroutine(DeSpawnCoroutine(consumable, ((SpawnIntervalPowerUpMin + SpawnIntervalPowerUpMax) / 2) * 10f));
            }
        }
    }

    IEnumerator DeSpawnCoroutine(ConsumableController consumable, float interval)
    {
        yield return new WaitForSeconds(interval);
        lock (lockObject)
        {
            Destroy(consumable);
            consumablePool--;
        }
    }

}
