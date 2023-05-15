using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public SnakeHeadController SnakeHeadController;
    public WorldController WorldController;
    public string InputHorizontalName, InputVerticalName;
    public Vector3 position = new Vector3(0, 0, -1);

    public int Score
    {
        get
        {
            if (gameObject.GetComponentInChildren<ScoreController>() != null)
                return gameObject.GetComponentInChildren<ScoreController>().Score;
            return 0;
        }
    }

    void Awake()
    {
        if (AudioManager.Instance)
            AudioManager.Instance.Play("Spawn");
    }

    void Start()
    {
        SnakeHeadController.Position = position;
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw(InputHorizontalName);
        float vertical = Input.GetAxisRaw(InputVerticalName);

        SnakeHeadController.PositionHandler(horizontal, vertical);
    }

    void OnDestroy()
    {
        Destroy(gameObject);
    }

    public void GameOver()
    {
        if (WorldController == null)
            return;
        WorldController.GameOver();
    }
}
