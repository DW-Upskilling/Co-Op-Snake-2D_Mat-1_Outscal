using UnityEngine;

public class ConsumableSpawner : MonoBehaviour
{
    public GameObject ConsumablePrefab;
    public GameWorldController GameWorldController;
    public PlayerController[] players;
    public Sprite[] FoodSprite;

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
        consumable.GameWorldController = GameWorldController;
        consumable.ConsumableSpawner = gameObject.GetComponent<ConsumableSpawner>();

        consumable.SetSprite(FoodSprite[Random.Range(0, FoodSprite.Length)]);
        consumable.SetRandomPosition();

    }
}