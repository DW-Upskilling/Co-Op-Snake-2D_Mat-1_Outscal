using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public GameObject SnakeHeadPrefab;
    public string InputHorizontalName, InputVerticalName;

    private GameObject snakeHead;

    void Awake()
    {

    }

    void Start()
    {
        snakeHead = Instantiate(SnakeHeadPrefab);
    }

    void Update()
    {
        inputHandler();
    }

    void inputHandler()
    {
        float horizontal = Input.GetAxisRaw(InputHorizontalName);
        float vertical = Input.GetAxisRaw(InputVerticalName);

        snakeHead.GetComponent<SnakeHeadController>().PositionHandler(horizontal, vertical);
    }
}