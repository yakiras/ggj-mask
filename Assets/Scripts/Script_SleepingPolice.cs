using System.Collections;
using UnityEngine;

public class SleepingPolice : MonoBehaviour
{
    public PlayerDisguise playerController;
    public GameStateManager gameStateManager;

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
        yield return new WaitForSeconds(1.5f);
        CheckDisguise();
    }

    void CheckDisguise()
    {
        switch (playerController.currentDisguise)
        {
            case "thief":
                // ENDING 2: prison
                gameStateManager.DisplayEnding(2);
                break;
            case "girl":
                SetAnimation(hearts);
                break;
            case "thug":
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
