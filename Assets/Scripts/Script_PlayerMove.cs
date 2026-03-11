using UnityEngine;

public class AutoWalker : MonoBehaviour
{
    public float moveSpeed = 5f; // 2.5f default
    public float rightBoundary = 20f; // Set this to the end of your level
    public float leftBoundary = -6f;    // Set this to the start of your level

    void Update()
    {
        if (!GameStateManager.Instance.stopMoving)
        {
            Move();
            CheckBoundaries();
        }
    }

    void Move()
    {
        // Calculate movement
        float step = moveSpeed * Time.deltaTime;

        if (!GameStateManager.Instance.secondTrip)
        {
            transform.Translate(Vector2.right * step);
        }
        else
        {
            transform.Translate(Vector2.left * step);
        }
    }

    void CheckBoundaries()
    {
        if (transform.position.x <= leftBoundary && GameStateManager.Instance.secondTrip)
        {
            // ENDING EVALUATION
            GameStateManager.Instance.EvaluateEnding();
        }
    }
}