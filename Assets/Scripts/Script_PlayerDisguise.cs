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

    private float frameRate; // seconds per frame

    private SpriteRenderer sr;
    private Sprite[] currentAnimation;
    private int currentFrame;
    private float timer;
    private bool playOnce = false;
    private Sprite[] nextAnimation;

    public AudioClip sfxPop;
    public AudioClip sfxCheers;
    public AudioClip sfxMoney;
    public AudioClip sfxKiss;
    private AudioSource audioSource;
    private AudioHelper audioHelper;

    void Start()
    {
        audioHelper = GetComponent<AudioHelper>();
        audioSource = GetComponent<AudioSource>();
        sr = GetComponent<SpriteRenderer>();
        frameRate = 1.0f / fps;
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
                    frameRate = 1.0f / fps;
                    audioHelper.StopLoop();

                    TransformDisguise("thief");
                }
                if (Keyboard.current.digit2Key.wasPressedThisFrame)
                {
                    frameRate = 1.0f / fps;
                    audioHelper.StopLoop();

                    TransformDisguise("girl");
                }
                if (Keyboard.current.digit3Key.wasPressedThisFrame)
                {
                    frameRate = 1.0f / fps;
                    audioHelper.StopLoop();

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
            if (!GameStateManager.Instance.stealAnimLocked)
                nextAnimation = thiefWalk;
            else
                nextAnimation = stealing;
        }
        if (newDisguise == "girl")
        {
            SetAnimation(girlTransform);
            if (!GameStateManager.Instance.kissAnimLocked)
                nextAnimation = girlWalk;
            else
                nextAnimation = blowKiss;
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

        audioHelper.PlayLoop(sfxMoney);
    }

    public void BlowKiss()
    {
        SetAnimation(blowKiss);
        currentDisguise = "girl";

        audioHelper.PlayLoop(sfxKiss);
    }

    public void Cheers()
    {
        SetAnimation(cheers);
        currentDisguise = "thug";

        audioHelper.PlayOnce(sfxCheers);
    }

    public void GetBeatUp()
    {
        audioHelper.StopLoop();
        StartCoroutine(SetAnimationWithDelay(beatUp));
    }

    public void DefaultThiefAnimation()
    {
        frameRate = 1.0f / fps;
        SetAnimation(thiefWalk);
        currentDisguise = "thief";

        audioHelper.StopLoop();
    }

    public void DefaultGirlAnimation()
    {
        frameRate = 1.0f / fps;
        SetAnimation(girlWalk);
        currentDisguise = "girl";

        audioHelper.StopLoop();
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

        if (newAnimation == blowKiss)
            frameRate = 0.3f;
        else
            frameRate = 1f / fps;

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
