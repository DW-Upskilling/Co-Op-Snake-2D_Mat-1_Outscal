using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public GameObject SnakeHeadPrefab;
    public string InputHorizontalName, InputVerticalName;
    public GameWorldController gameWorldController;

    private GameMode gameMode;
    public GameMode GameMode { get { return gameMode; } set { gameMode = value; } }

    private GameObject snakeHead;

    void Awake()
    {

    }

    void Start()
    {
        snakeHead = Instantiate(SnakeHeadPrefab);
        snakeHead.GetComponent<SnakeHeadController>().GameWorldController = gameWorldController;
        snakeHead.GetComponent<SnakeHeadController>().PlayerController = gameObject.GetComponent<PlayerController>();
    }

    void Update()
    {
        // Debug.Log(
        //     "Top: " + gameWorldController.GetTopEdgePosition() +
        //     "\tBottom: " + gameWorldController.GetBottomEdgePosition() +
        //     "\tLeft: " + gameWorldController.GetLeftEdgePosition() +
        //     "\tRight: " + gameWorldController.GetRightEdgePosition()
        // );
        inputHandler();
    }

    void inputHandler()
    {
        float horizontal = Input.GetAxisRaw(InputHorizontalName);
        float vertical = Input.GetAxisRaw(InputVerticalName);

        snakeHead.GetComponent<SnakeHeadController>().PositionHandler(horizontal, vertical);
    }

    public bool isAlive()
    {
        return true;
    }
}