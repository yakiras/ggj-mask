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

    public Sprite[] thiefTransform;
    public Sprite[] girlTransform;
    public Sprite[] thugTransform;

    public InGameUI gameUI;

    public float fps = 5.0f;
    public float coolDown = 1.5f;
    public bool inputEnabled = true;

    private float currentFps;
    private float frameRate; // seconds per frame

    private SpriteRenderer sr;
    private Sprite[] currentAnimation;
    private int currentFrame;
    private float timer;
    private bool playOnce = false;
    private Sprite[] nextAnimation;
    private bool sfxAnimPlaying;

    public AudioClip sfxPop;
    public AudioClip sfxMoney;
    public AudioClip sfxKiss;
    private AudioClip currentSfxLoop;
    private AudioSource audioSource;
    private Coroutine sfxRoutine;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        sr = GetComponent<SpriteRenderer>();
        currentFps = fps;
        frameRate = 1.0f / currentFps;
        currentDisguise = "thief";
        DefaultThiefAnimation(); // default animation
        sfxAnimPlaying = false;
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

                    TransformDisguise("thief");
                }
                if (Keyboard.current.digit2Key.wasPressedThisFrame)
                {
                    currentFps = fps;
                    frameRate = 1.0f / currentFps;

                    TransformDisguise("girl");
                }
                if (Keyboard.current.digit3Key.wasPressedThisFrame)
                {
                    currentFps = fps;
                    frameRate = 1.0f / currentFps;

                    TransformDisguise("thug");
                }
            }
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
        sfxAnimPlaying = false;
        audioSource.Stop();
        audioSource.PlayOneShot(sfxPop);
    }

    private void TransformDisguise(string newDisguise)
    {
        PlayTransformSFX();
        currentDisguise = newDisguise;
        gameUI.SwapIcon(currentDisguise);
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
        StartCoroutine(StartCooldown());
    }

    public void Steal()
    {
        SetAnimation(stealing);
        currentDisguise = "thief";

        currentSfxLoop = sfxMoney;
        StartSFXLoop();
    }

    public void BlowKiss()
    {
        currentFps = 3f;
        frameRate = 1f / currentFps;
        SetAnimation(blowKiss);
        currentDisguise = "girl";

        currentSfxLoop = sfxKiss;
        StartSFXLoop();
    }

    public void Cheers()
    {
        SetAnimation(cheers);
        currentDisguise = "thug";
    }

    public void GetBeatUp()
    {
        StartCoroutine(SetAnimationWithDelay(beatUp));
    }

    private void StartSFXLoop()
    {
        sfxAnimPlaying = true;
        if (sfxRoutine == null)
            sfxRoutine = StartCoroutine(PlaySFXLoop());
    }
    private IEnumerator PlaySFXLoop()
    {
        while (sfxAnimPlaying)
        {
            audioSource.PlayOneShot(currentSfxLoop);
            yield return new WaitForSeconds(0.6f);
        }
    }

    public void DefaultThiefAnimation()
    {
        currentFps = fps;
        frameRate = 1f / currentFps;
        SetAnimation(thiefWalk);
        currentDisguise = "thief";
        sfxAnimPlaying = false;
    }

    public void DefaultGirlAnimation()
    {
        currentFps = fps;
        frameRate = 1f / currentFps;
        SetAnimation(girlWalk);
        currentDisguise = "girl";
        sfxAnimPlaying = false;
    }

    private IEnumerator StartCooldown()
    {
        gameUI.DisplayCooldown(coolDown);
        inputEnabled = false;
        yield return new WaitForSeconds(coolDown);
        inputEnabled = true;
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
