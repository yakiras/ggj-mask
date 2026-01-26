using UnityEngine;

public class AutoWalker : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rightBoundary = 20f; // Set this to the end of your level
    public float leftBoundary = 0f;    // Set this to the start of your level

    private bool movingRight = true;

    void Update()
    {
        Move();
        CheckBoundaries();
    }

    void Move()
    {
        // Calculate movement
        float step = moveSpeed * Time.deltaTime;

        if (movingRight)
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
        // Flip direction if boundary is hit
        if (transform.position.x >= rightBoundary && movingRight)
        {
            movingRight = false;
            FlipSprite();
        }
        else if (transform.position.x <= leftBoundary && !movingRight)
        {
            movingRight = true;
            FlipSprite();
        }
    }

    void FlipSprite()
    {
        // Flips the character visually
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}