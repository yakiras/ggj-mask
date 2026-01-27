using UnityEngine;
using UnityEngine.InputSystem;

public class SpriteSwitcher : MonoBehaviour
{
    public Sprite[] thiefWalk;
    public Sprite[] girlWalk;
    public Sprite[] thugWalk;
    public float fps = 5.0f;

    private float frameRate; // seconds per frame

    private SpriteRenderer sr;
    private Sprite[] currentAnimation;
    private int currentFrame;
    private float timer;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        frameRate = 1.0f / fps;
        SetAnimation(thiefWalk); // default animation
    }

    void Update()
    {
        // Switch animations with keys
        if (Keyboard.current.digit1Key.wasPressedThisFrame) SetAnimation(thiefWalk);
        if (Keyboard.current.digit2Key.wasPressedThisFrame) SetAnimation(girlWalk);
        if (Keyboard.current.digit3Key.wasPressedThisFrame) SetAnimation(thugWalk);

        // Update frame
        if (currentAnimation == null || currentAnimation.Length == 0) return;

        timer += Time.deltaTime;
        if (timer >= frameRate)
        {
            timer = 0f;
            currentFrame = (currentFrame + 1) % currentAnimation.Length;
            sr.sprite = currentAnimation[currentFrame];
        }
    }

    void SetAnimation(Sprite[] newAnimation)
    {
        if (newAnimation == currentAnimation) return;
        currentAnimation = newAnimation;
        currentFrame = 0;
        timer = 0f;
        if (currentAnimation.Length > 0)
            sr.sprite = currentAnimation[0];
    }
}
