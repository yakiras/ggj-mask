using System.Collections;
using UnityEngine;

public class BarThug : MonoBehaviour
{
    public PlayerDisguise playerController;
    public GameStateManager gameStateManager;

    public Sprite[] playerDrink;
    public Sprite[] thugIdle;
    public Sprite[] thugHearts;
    public Sprite[] thugFight;

    public bool thief = false;
    public bool girl = false;
    public bool thug = false;

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

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Enter hit trigger");
        if (other.CompareTag("Player"))
        {
            CheckDisguiseR1();
        }
    }

    void CheckDisguiseR1()
    {
        switch (playerController.currentDisguise)
        {
            case "thief":
                // ENDING 1: get beat up by thug
                playerController.SetAnimation(thugFight);
                StartCoroutine(gameStateManager.DisplayEnding(1));
                gameObject.SetActive(false);
                break;
            case "girl":
                girl = true;
                Debug.Log("hearts");
                SetAnimation(thugHearts);
                break;
            case "thug":
                thug = true;
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
