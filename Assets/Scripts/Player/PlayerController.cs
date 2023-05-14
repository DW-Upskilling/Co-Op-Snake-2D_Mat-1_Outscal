using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public SnakeHeadController SnakeHeadController;
    public string InputHorizontalName, InputVerticalName;

    void Update()
    {
        float horizontal = Input.GetAxisRaw(InputHorizontalName);
        float vertical = Input.GetAxisRaw(InputVerticalName);

        SnakeHeadController.PositionHandler(horizontal, vertical);
    }
}
