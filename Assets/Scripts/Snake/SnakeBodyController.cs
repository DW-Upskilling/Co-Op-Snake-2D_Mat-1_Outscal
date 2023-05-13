using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBodyController : MonoBehaviour
{
    public GameObject SnakeBodyPrefab;
    private GameWorldController gameWorldController;
    private PlayerController playerController;
    private GameObject previous, next;

    const float SPRITE_SIZE = 1.06f;

    public GameObject Parent { get { return previous; } set { previous = value; } }
    public GameObject Child { get { return next; } set { next = value; } }
    public GameWorldController GameWorldController { set { gameWorldController = value; } }
    public PlayerController PlayerController { set { playerController = value; } }

    void Start()
    {
        gameObject.GetComponent<Transform>().position = new Vector3(0, 0, -1);
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.GetComponent<SnakeHeadController>() != null)
        {
            collider.gameObject.GetComponent<SnakeHeadController>().Kill(gameObject);
        }
    }

    public void UpdatePosition()
    {
        Transform transform = gameObject.GetComponent<Transform>();

        Vector3 prevPosition = previous.GetComponent<Transform>().position;
        Quaternion prevRotation = previous.GetComponent<Transform>().rotation;

        Vector3 currentPosition = transform.position;
        Quaternion currentRotation = transform.rotation;

        /// Calculate the average position and rotation between the previous, current, and next segments
        Vector3 position = (prevPosition + currentPosition) / 2f;
        // Vector3 position = prevPosition - (currentPosition - prevPosition).normalized * SPRITE_SIZE;
        Quaternion rotation = Quaternion.Lerp(prevRotation, currentRotation, 0.5f);

        // Calculate the offset based on sprite size and rotation
        Vector3 offset = Quaternion.Euler(0f, 0f, rotation.eulerAngles.z) * new Vector3(0f, SPRITE_SIZE, 0f);

        // Update the current segment's position with the offset
        position = prevPosition + (-offset);

        // Wrap the position if it goes beyond the game screen edges
        position = WrapPosition(position);

        // Update the current segment's position and rotation
        transform.position = position;
        transform.rotation = rotation;

        // Recursively update the position of the previous segments
        if (previous != null && previous.GetComponent<SnakeBodyController>() != null)
        {
            previous.GetComponent<SnakeBodyController>().UpdatePosition();
        }
    }

    Vector3 WrapPosition(Vector3 position)
    {
        float leftEdge = gameWorldController.GetLeftEdgePosition() + SPRITE_SIZE / 4f;
        float rightEdge = gameWorldController.GetRightEdgePosition() - SPRITE_SIZE / 4f;
        float bottomEdge = gameWorldController.GetBottomEdgePosition() + SPRITE_SIZE / 4f;
        float topEdge = gameWorldController.GetTopEdgePosition() - SPRITE_SIZE / 4f;

        if (position.x < leftEdge)
            position.x = rightEdge;
        else if (position.x > rightEdge)
            position.x = leftEdge;
        else if (position.y < bottomEdge)
            position.y = topEdge;
        else if (position.y > topEdge)
            position.y = bottomEdge;

        return position;
    }

}
