using System.Collections;
using UnityEngine;

public class SleepingPolice : MonoBehaviour
{
    public PlayerDisguise playerController;

    public Sprite[] idle;
    public Sprite[] wake;
    public Sprite[] hearts;
    public float frameRate = 2.0f; // seconds per frame

    private SpriteRenderer sr;
    private Sprite[] currentAnimation;
    private int currentFrame;
    private float timer;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        SetAnimation(idle); // default animation
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        StartCoroutine(WaitBeforeCheck());
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        CheckDisguise();
    }

    void Update()
    {
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

    IEnumerator WaitBeforeCheck()
    {
        SetAnimation(wake);
        yield return new WaitForSeconds(1.0f);
        CheckDisguise();
    }

    void CheckDisguise()
    {
        switch (playerController.currentDisguise)
        {
            case "thief":
                // ENDING 1: alone & prison
                SetAnimation(wake);
                GameStateManager.Instance.DisplayEnding(1);
                break;
            case "girl":
                SetAnimation(hearts);
                break;
            case "thug":
                // ENDING 1: alone & prison
                SetAnimation(wake);
                GameStateManager.Instance.DisplayEnding(1);
                break;
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
