using UnityEngine;

public class ConsumableSpawner : MonoBehaviour
{
    public GameObject ConsumablePrefab;
    public GameWorldController GameWorldController;
    public PlayerController[] players;
    public Sprite[] GainerFoodSprite;
    public Sprite[] BurnerFoodSprite;
    public Sprite[] BoostFoodSprite;

    void Awake()
    {

    }

    void Start()
    {
        for (int i = 0; i < 10; i++)
            Spawn();

    }

    void Update()
    {
        if (isPlayersAlive())
        {
            // Debug.Log("We are cool");
        }
    }

    bool isPlayersAlive()
    {
        for (int i = 0; i < players.Length; i++)
        {
            if (!players[i].isAlive())
                return false;
        }
        return true;
    }

    public void Spawn()
    {
        ConsumableController consumable = Instantiate(ConsumablePrefab).GetComponent<ConsumableController>();

        consumable.gameWorldController = GameWorldController;
        consumable.consumableSpawner = gameObject.GetComponent<ConsumableSpawner>();

        int index = Random.Range(0, 2);

        if (index == 1)
        {
            consumable.consumableType = ConsumableType.Burner;
            consumable.SetSprite(BurnerFoodSprite[Random.Range(0, BurnerFoodSprite.Length)]);
        }
        else
        {
            consumable.consumableType = ConsumableType.Gainer;
            consumable.SetSprite(GainerFoodSprite[Random.Range(0, GainerFoodSprite.Length)]);
        }

        consumable.SetRandomPosition();

    }
}