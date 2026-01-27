using UnityEngine;
using UnityEngine.InputSystem;

public class JewelryBoss : MonoBehaviour
{
    public PlayerDisguise playerController;
    public GameStateManager gameStateManager;

    public Sprite[] idle;
    public Sprite[] hearts;
    public Sprite[] shock;
    public Sprite[] sad;

    public bool thief = false;
    public bool girl = false;
    public bool thug = false;
    private bool secondRound = false;
    private float frameRate = 0.5f; // seconds per frame

    private SpriteRenderer sr;
    private Sprite[] currentAnimation;
    private int currentFrame;
    private float timer;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        SetAnimation(idle); // default animation
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!secondRound) CheckDisguiseR1();
            else CheckDisguiseR2();
        }
    }
    void CheckDisguiseR1()
    {
        switch (playerController.currentDisguise)
        {
            case "thief":
                thief = true;
                SetAnimation(shock);
                break;
            case "girl":
                girl = true;
                SetAnimation(hearts);
                break;
            case "thug":
                thug = true;
                SetAnimation(sad);
                gameStateManager.hasKey = true;
                break;
        }
        secondRound = true;
    }

    void CheckDisguiseR2()
    {
        switch (playerController.currentDisguise)
        {
            case "thief":
                thief = true;
                break;
            case "girl":
                girl = true;
                break;
            case "thug":
                thug = true;
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
