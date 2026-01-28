using UnityEngine;

public class AutoWalker : MonoBehaviour
{
    public float moveSpeed = 5f;
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
            if (GameStateManager.Instance.money < GameStateManager.Instance.moneyThreshold)
            {
                StartCoroutine(GameStateManager.Instance.DisplayEnding(0));
            }
            else
            {
                StartCoroutine(GameStateManager.Instance.DisplayEnding(4));
            }
        }
    }
}