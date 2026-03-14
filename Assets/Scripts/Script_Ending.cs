using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class Ending : MonoBehaviour
{
    public Canvas canvas;
    public float fps = 3.0f;
    public Sprite[] ending0; // alone & prison
    public Sprite[] ending1; // alone & poor
    public Sprite[] ending2; // alone & rich
    public Sprite[] ending3; // fucking dead
    public Sprite[] ending4; // bro & prison
    public Sprite[] ending5; // bro & poor
    public Sprite[] ending6; // bro & rich

    public Sprite[] ending3OP;

    public AudioClip sfxGunshot;
    public AudioClip bgmSad;
    public AudioClip bgmHappy;

    public string menuScene;
    public string gameScene;
    public string endScene;

    private float frameRate; // seconds per frame

    private SpriteRenderer sr;
    private Sprite[] currentAnimation;
    private int currentFrame;
    private float timer;
    private bool playOnce;
    private AudioSource audioSource;

    //private int ending;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        sr = GetComponent<SpriteRenderer>();
        frameRate = 1.0f / fps;
        canvas.enabled = false;
        playOnce = false;

        switch (GameStateManager.Instance.currentEnding)
        {
            case 2:
                audioSource.loop = true;
                audioSource.PlayOneShot(bgmHappy);
                break;
            case 3:
                audioSource.loop = false;
                audioSource.PlayOneShot(sfxGunshot);
                break;
            case 6:
                audioSource.loop = true;
                audioSource.PlayOneShot(bgmHappy);
                break;
            default:
                audioSource.loop = true;
                audioSource.PlayOneShot(bgmSad);
                break;
        }

        switch (GameStateManager.Instance.currentEnding)
        //switch (ending)
        {
            case 0:
                SetAnimation(ending0);
                break;
            case 1:
                SetAnimation(ending1);
                break;
            case 2:
                SetAnimation(ending2);
                break;
            case 3:
                playOnce = true;
                frameRate = 1.0f / 7.0f;
                SetAnimation(ending3OP);
                break;
            case 4:
                SetAnimation(ending4);
                break;
            case 5:
                SetAnimation(ending5);
                break;
            case 6:
                SetAnimation(ending6);
                break;
            default:
                break;
        }

        StartCoroutine(WaitAndDisplayUI());
    }

    void Update()
    {
        if (currentAnimation == null || currentAnimation.Length == 0) return;

        timer += Time.deltaTime;
        if (timer >= frameRate)
        {
            timer = 0f;

            // PLAY ONCE MODE - only for ending 3 opening
            if (playOnce)
            {
                currentFrame++;

                if (currentFrame >= currentAnimation.Length)
                {
                    // switch animation
                    frameRate = 1.0f / fps;
                    currentAnimation = ending3;
                    currentFrame = 0;
                    playOnce = false;
                    audioSource.loop = true;
                    audioSource.PlayOneShot(bgmSad);
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

    void SetAnimation(Sprite[] newAnimation)
    {
        if (newAnimation == currentAnimation) return;
        currentAnimation = newAnimation;
        currentFrame = 0;
        timer = 0f;
        if (currentAnimation.Length > 0)
        {
            sr.sprite = currentAnimation[0];
        }
    }

    IEnumerator WaitAndDisplayUI()
    {
        yield return new WaitForSeconds(3f);
        canvas.enabled = true;
    }

    public void Retry()
    {
        GameStateManager.Instance.ResetGame();
        SceneManager.LoadScene(gameScene);
    }

    public void Quit()
    {
        GameStateManager.Instance.ResetGame();
        SceneManager.LoadScene(menuScene);
    }
}
