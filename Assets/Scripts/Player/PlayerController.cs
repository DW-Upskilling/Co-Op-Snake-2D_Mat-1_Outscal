using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public SnakeHeadController SnakeHeadController;
    public WorldController WorldController;
    public string InputHorizontalName, InputVerticalName;
    public Vector3 position = new Vector3(0, 0, -1);

    public GameObject Shield, ScoreBoost, SpeedBoost;

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

        if (SnakeHeadController.ConsumablePowerUpTypeFind(ConsumablePowerUpType.Shield) == ConsumablePowerUpType.Shield)
            Shield.SetActive(true);
        else
            Shield.SetActive(false);

        if (SnakeHeadController.ConsumablePowerUpTypeFind(ConsumablePowerUpType.ScoreBoost) == ConsumablePowerUpType.ScoreBoost)
            ScoreBoost.SetActive(true);
        else
            ScoreBoost.SetActive(false);

        if (SnakeHeadController.ConsumablePowerUpTypeFind(ConsumablePowerUpType.SpeedUp) == ConsumablePowerUpType.SpeedUp)
            SpeedBoost.SetActive(true);
        else
            SpeedBoost.SetActive(false);
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
