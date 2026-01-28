using System.Collections;
using UnityEngine;

public class BarThug : MonoBehaviour
{
    public PlayerDisguise playerController;

    public Sprite[] playerDrink;
    public Sprite[] thugIdle;
    public Sprite[] thugHearts;
    public Sprite[] thugFight;

    private SpriteRenderer sr;
    private Sprite[] currentAnimation;
    private float frameRate; // seconds per frame
    private int currentFrame;
    private float timer;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        frameRate = 0.2f;
        SetAnimation(thugIdle); // default animation
    }

    private void Update()
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CheckDisguise();
        }
    }

    void CheckDisguise()
    {
        switch (playerController.currentDisguise)
        {
            case "thief":
                // ENDING 1: get beat up by thug
                playerController.SetAnimation(thugFight);
                StartCoroutine(GameStateManager.Instance.DisplayEnding(1));
                gameObject.SetActive(false);
                break;
            case "girl":
                SetAnimation(thugHearts);
                if (playerController.transform.position.x > transform.position.x)
                    sr.flipX = true;
                else sr.flipX = false;
                break;
            case "thug":
                StartCoroutine(playerController.SetAnimationWithDelay(playerDrink, 3.0f));
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
