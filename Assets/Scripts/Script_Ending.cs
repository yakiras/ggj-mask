using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class Ending : MonoBehaviour
{
    public Canvas canvas;
    public float fps = 3.0f;
    public Sprite[] ending0; // alone & poor
    public Sprite[] ending1; // alone & prison
    public Sprite[] ending2; // bro & prison
    public Sprite[] ending3OP;
    public Sprite[] ending3; // fucking dead
    public Sprite[] ending4; // rich

    public string menuScene;
    public string gameScene;
    public string endScene;

    private float frameRate; // seconds per frame

    private SpriteRenderer sr;
    private Sprite[] currentAnimation;
    private int currentFrame;
    private float timer;
    private bool playOnce;

    //private int ending;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        frameRate = 1.0f / fps;
        canvas.enabled = false;
        playOnce = false;

        //ending = 3;

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
