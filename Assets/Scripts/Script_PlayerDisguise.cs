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

    public Sprite[] blowKiss;
    public Sprite[] cheers;
    public Sprite[] stealing;
    public Sprite[] beatUp;

    public float fps = 5.0f;
    public float coolDown = 2.0f;
    public bool inputEnabled = true;

    private float currentFps;
    private float frameRate; // seconds per frame

    private SpriteRenderer sr;
    private Sprite[] currentAnimation;
    private int currentFrame;
    private float timer;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        currentFps = fps;
        frameRate = 1.0f / currentFps;
        currentDisguise = "thief";
        DefaultThiefAnimation(); // default animation
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
                    currentFps = fps;
                    frameRate = 1.0f / currentFps;

                    currentDisguise = "thief";
                    StartCoroutine(SetAnimationWithDelay(thiefWalk));
                }
                if (Keyboard.current.digit2Key.wasPressedThisFrame)
                {
                    currentFps = fps;
                    frameRate = 1.0f / currentFps;

                    currentDisguise = "girl";
                    StartCoroutine(SetAnimationWithDelay(girlWalk));
                }
                if (Keyboard.current.digit3Key.wasPressedThisFrame)
                {
                    currentFps = fps;
                    frameRate = 1.0f / currentFps;

                    currentDisguise = "thug";
                    StartCoroutine(SetAnimationWithDelay(thugWalk));
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
    public void Steal()
    {
        StartCoroutine(SetAnimationWithDelay(stealing));
        currentDisguise = "thief";
    }

    public void BlowKiss()
    {
        currentFps = 3f;
        frameRate = 1f / currentFps;

        StartCoroutine(SetAnimationWithDelay(blowKiss));
        currentDisguise = "girl";
    }

    public void Cheers()
    {
        StartCoroutine(SetAnimationWithDelay(cheers));
        currentDisguise = "thug";
    }

    public void GetBeatUp()
    {
        StartCoroutine(SetAnimationWithDelay(beatUp));
    }

    public void DefaultThiefAnimation()
    {
        currentFps = fps;
        frameRate = 1f / currentFps;
        SetAnimation(thiefWalk);
        currentDisguise = "thief";
    }

    public void DefaultGirlAnimation()
    {
        currentFps = fps;
        frameRate = 1f / currentFps;
        SetAnimation(girlWalk);
        currentDisguise = "girl";
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

    public IEnumerator SetAnimationWithDelay(Sprite[] anim)
    {
        inputEnabled = false;
        SetAnimation(anim);
        yield return new WaitForSeconds(coolDown);
        inputEnabled = true;
    }

    public void FlipSprite()
    {
        // Flips the character visually
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}
