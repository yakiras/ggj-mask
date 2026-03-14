using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTutorial : MonoBehaviour
{
    public Sprite[] thiefWalk;
    public Sprite[] girlWalk;
    public Sprite[] thugWalk;

    public Sprite[] thiefTransform;
    public Sprite[] girlTransform;
    public Sprite[] thugTransform;

    public AudioClip sfxPop;
    private AudioSource audioSource;

    public float fps = 5.0f;
    private float frameRate; // seconds per frame

    private SpriteRenderer sr;
    private Sprite[] currentAnimation;
    private Sprite[] nextAnimation;
    private int currentFrame;
    private float timer;
    private bool playOnce = false;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        frameRate = 1.0f / fps;
        SetAnimation(thiefWalk);
    }

    void Update()
    {
        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            TransformDisguise("thief");
        }
        if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            TransformDisguise("girl");
        }
        if (Keyboard.current.digit3Key.wasPressedThisFrame)
        {
            TransformDisguise("thug");
        }

        if (currentAnimation == null || currentAnimation.Length == 0) return;

        timer += Time.deltaTime;
        if (timer >= frameRate)
        {
            timer = 0f;

            // PLAY ONCE MODE
            if (playOnce)
            {
                currentFrame++;

                if (currentFrame >= currentAnimation.Length)
                {
                    // switch animation
                    frameRate = 1.0f / fps;
                    currentAnimation = nextAnimation;
                    currentFrame = 0;
                    playOnce = false;
                    return;
                }
            }
            else
            {
                // LOOP MODE
                currentFrame = (currentFrame + 1) % currentAnimation.Length;
            }

            sr.sprite = currentAnimation[currentFrame];
        }
    }

    private void PlayTransformSFX()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(sfxPop);
    }

    private void TransformDisguise(string newDisguise)
    {
        PlayTransformSFX();
        playOnce = true;
        frameRate = 1f / 5f;
        if (newDisguise == "thief")
        {
            SetAnimation(thiefTransform);
            nextAnimation = thiefWalk;
        }
        if (newDisguise == "girl")
        {
            SetAnimation(girlTransform);
            nextAnimation = girlWalk;
        }
        if (newDisguise == "thug")
        {
            SetAnimation(thugTransform);
            nextAnimation = thugWalk;
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
}
