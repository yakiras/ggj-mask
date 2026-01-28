using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDisguise : MonoBehaviour
{
    public string currentDisguise;
    public Sprite[] thiefWalk;
    public Sprite[] girlWalk;
    public Sprite[] thugWalk;
    public Sprite[] idle;
    public Sprite[] stealing;

    public float fps = 5.0f;
    public bool inputEnabled = true;

    private float frameRate; // seconds per frame

    private SpriteRenderer sr;
    private Sprite[] currentAnimation;
    private int currentFrame;
    private float timer;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        frameRate = 1.0f / fps;
        currentDisguise = "thief";
        DefaultAnimation(); // default animation
    }

    void Update()
    {
        if (GameStateManager.Instance.stopMoving)
        {
            if (GameStateManager.Instance.atBankers)
                SetAnimation(idle);
        }
        else
        {
            // Switch animations with keys
            if (inputEnabled)
            {
                if (Keyboard.current.digit1Key.wasPressedThisFrame)
                {
                    currentDisguise = "thief";
                    SetAnimation(thiefWalk);
                }
                if (Keyboard.current.digit2Key.wasPressedThisFrame)
                {
                    currentDisguise = "girl";
                    SetAnimation(girlWalk);
                }
                if (Keyboard.current.digit3Key.wasPressedThisFrame)
                {
                    currentDisguise = "thug";
                    SetAnimation(thugWalk);
                }
            }
        }

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

    public void SetAnimation(Sprite[] newAnimation)
    {
        if (newAnimation == currentAnimation) return;
        currentAnimation = newAnimation;
        currentFrame = 0;
        timer = 0f;
        if (currentAnimation.Length > 0)
            sr.sprite = currentAnimation[0];
    }

    public IEnumerator SetAnimationWithDelay(Sprite[] anim, float seconds)
    {
        inputEnabled = false;
        SetAnimation(anim);
        yield return new WaitForSeconds(seconds);
        inputEnabled = true;
    }

    public void StartStealing()
    {
        inputEnabled = false;
        SetAnimation(stealing);
    }

    public void StopStealing()
    {
        inputEnabled = true;
        SetAnimation(thiefWalk);
    }

    public void DefaultAnimation()
    {
        SetAnimation(thiefWalk);
    }

    public void FlipSprite()
    {
        // Flips the character visually
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}
