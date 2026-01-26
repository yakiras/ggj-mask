using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public float lookAheadDistance = 3f; // Distance the camera stays ahead

    // These should match your Player's boundaries
    public float leftLimit = 0f;
    public float rightLimit = 20f;

    void LateUpdate()
    {
        if (target == null) return;

        // 1. Determine direction based on player scale (from the FlipSprite method)
        // If scale.x is positive, player is moving right.
        float direction = target.localScale.x > 0 ? 1f : -1f;

        // 2. Calculate the "Target" position with the look-ahead offset
        float targetX = target.position.x + (direction * lookAheadDistance);

        // 3. Clamp the camera so it doesn't go past the world edges
        // This makes the camera "wait" at the edge while the player catches up
        float clampedX = Mathf.Clamp(targetX, leftLimit, rightLimit);

        // 4. Apply Smoothing
        Vector3 desiredPosition = new Vector3(clampedX, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
    }
}