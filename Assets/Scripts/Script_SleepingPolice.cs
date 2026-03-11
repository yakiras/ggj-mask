using System.Collections;
using UnityEngine;

public class SleepingPolice : MonoBehaviour
{
    public PlayerDisguise playerController;

    public Sprite[] idle;
    public Sprite[] wake;
    public Sprite[] hearts;
    public Sprite[] awake;
    public float frameRate = 2.0f; // seconds per frame

    private SpriteRenderer sr;
    private Sprite[] currentAnimation;
    private int currentFrame;
    private float timer;
    private bool canCheckDisguise;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        SetAnimation(idle); // default animation
        canCheckDisguise = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!GameStateManager.Instance.secondTrip)
            StartCoroutine(WaitBeforeCheck());
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (GameStateManager.Instance.secondTrip)
            CheckDisguiseR2();
        else if (canCheckDisguise)
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
        canCheckDisguise = true;
    }

    private void CheckDisguise()
    {
        switch (playerController.currentDisguise)
        {
            case "thief":
                // ENDING 0: alone & poor
                GameStateManager.Instance.InitializeEnding(0);
                break;
            case "girl":
                SetAnimation(hearts);
                break;
            case "thug":
                // ENDING 0: alone & poor
                GameStateManager.Instance.InitializeEnding(0);
                break;
        }
    }

    private void CheckDisguiseR2()
    {
        if (!playerController.currentDisguise.Equals("girl"))
        {
            // ENDING 4: bro & prison
            GameStateManager.Instance.InitializeEnding(4);
        }
    }

    public void StayAwake()
    {
        Vector3 pos = transform.localPosition;
        pos.x = -2f;
        pos.y = 0;
        transform.localPosition = pos;
        sr.sortingLayerName = "NPC(inside)";
        SetAnimation(awake);
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
