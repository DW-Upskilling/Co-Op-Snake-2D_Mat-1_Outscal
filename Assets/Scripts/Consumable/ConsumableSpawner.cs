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
        // Randomly select a consumable type
        Array ConsumableTypes = Enum.GetValues(typeof(ConsumableType));
        int randomIndex = UnityEngine.Random.Range(0, ConsumableTypes.Length);
        ConsumableType consumableType = (ConsumableType)ConsumableTypes.GetValue(randomIndex);

        // Instantiate the consumable prefab
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
                // If power-up type is selected, destroy the consumable and try spawning a food consumable instead
                Destroy(consumable);
                consumable = SpawnFood();
                break;
        }

        return consumable;
    }

    ConsumableController SpawnPowerUp()
    {
        // Randomly select a power-up type
        Array ConsumablePowerUpTypes = Enum.GetValues(typeof(ConsumablePowerUpType));
        int randomIndex = UnityEngine.Random.Range(0, ConsumablePowerUpTypes.Length);
        ConsumablePowerUpType consumablePowerUpType = (ConsumablePowerUpType)ConsumablePowerUpTypes.GetValue(randomIndex);

        // Instantiate the consumable prefab
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

            // Check if the consumable pool has reached the maximum limit
            if (consumablePool >= MaxConsumables)
                continue;

            // Lock the consumable pool to ensure thread safety
            lock (lockObject)
            {
                // Spawn a food consumable and increment
                ConsumableController consumable = SpawnFood();
                consumablePool++;

                // Start the despawn coroutine for the spawned consumable
                StartCoroutine(DeSpawnCoroutine(consumable, SpawnIntervalFood * 10f));
            }

        }
    }

    IEnumerator SpawnPowerUpCoroutine()
    {
        while (true)
        {
            // Determine the current interval for spawning power-ups
            float currentInterval = UnityEngine.Random.Range(SpawnIntervalPowerUpMin, SpawnIntervalPowerUpMax + 1f);
            yield return new WaitForSeconds(currentInterval);

            // Check if the consumable pool has reached the maximum limit
            if (consumablePool >= MaxConsumables)
                continue;

            // Lock the consumable pool to ensure thread safety
            lock (lockObject)
            {
                // Spawn a power-up consumable
                ConsumableController consumable = SpawnPowerUp();
                consumablePool++;

                // Start the despawn coroutine for the spawned consumable
                StartCoroutine(DeSpawnCoroutine(consumable, ((SpawnIntervalPowerUpMin + SpawnIntervalPowerUpMax) / 2) * 10f));
            }
        }
    }

    IEnumerator DeSpawnCoroutine(ConsumableController consumable, float maxTimeInSeconds)
    {
        float startTime = Time.time;
        float elapsedTime = 0f;

        while (consumable != null && elapsedTime < maxTimeInSeconds)
        {
            elapsedTime = Time.time - startTime;

            // Calculate decrease rates based on elapsed time
            float remainingOpacity = (maxTimeInSeconds - elapsedTime) / maxTimeInSeconds;
            float remainingScale = (maxTimeInSeconds - elapsedTime) / maxTimeInSeconds;

            // Invoke the LifeTime of the consumable
            consumable.LifeTime(remainingOpacity, remainingScale);

            yield return null;
        }
        lock (lockObject)
        {
            if (consumable != null)
                Destroy(consumable);
            consumablePool--;
        }
    }

}
