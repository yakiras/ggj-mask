using System.Collections;
using UnityEngine;

public class BarThug : MonoBehaviour
{
    public PlayerDisguise playerController;
    public AutoWalker playerMovement;

    public Sprite[] thugIdle;
    public Sprite[] thugHearts;
    public Sprite[] thugFight;

    private bool following = false;

    private SpriteRenderer sr;
    private Sprite[] currentAnimation;
    private float frameRate; // seconds per frame
    private int currentFrame;
    private float timer;

    public AudioClip sfxHearts;
    public AudioClip sfxFight;
    public AudioClip sfxBro;
    private AudioHelper audioHelper;

    void Start()
    {
        audioHelper = GetComponent<AudioHelper>();
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

        if (following && !GameStateManager.Instance.stopMoving)
        {
            Move();
            if (!GameStateManager.Instance.secondTrip)
                CheckDisguiseFollowing();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!GameStateManager.Instance.secondTrip)
                CheckDisguise();
        }

        if (collision.CompareTag("Popo"))
        {
            audioHelper.PlayOnce(sfxFight);

            SetAnimation(thugFight);
            following = false;
            GameStateManager.Instance.broFollowing = false;

            collision.gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (GameStateManager.Instance.secondTrip)
            {
                if (GameStateManager.Instance.alertLevel > 0)
                {
                    Debug.Log("alert > 0");
                    GameStateManager.Instance.broFollowing = true;
                    sr.sortingLayerName = "Player";
                    audioHelper.PlaySingle(sfxBro);
                }
            }

            if (GameStateManager.Instance.broFollowing)
            {
                following = true;
            }
        }
    }

    void CheckDisguise()
    {
        switch (playerController.currentDisguise)
        {
            case "thief":
                // ENDING 1: alone & broke
                BeatUpPlayer();
                GameStateManager.Instance.InitializeEnding(1);
                break;
            case "girl":
                audioHelper.PlayOnce(sfxHearts);
                SetAnimation(thugHearts);
                GameStateManager.Instance.broFollowing = true;
                sr.sortingLayerName = "NPC(outside)";
                if (playerController.transform.position.x > transform.position.x)
                    sr.flipX = true;
                else sr.flipX = false;
                break;
            case "thug":
                playerController.Cheers();
                break;
        }
    }

    void CheckDisguiseFollowing()
    {
        if (!playerController.currentDisguise.Equals("girl"))
        {
            BeatUpPlayer();
        }
    }

    void BeatUpPlayer()
    {
        audioHelper.PlayOnce(sfxFight, 0.5f);
        playerController.GetBeatUp();
        sr.enabled = false;
        GameStateManager.Instance.InitializeEnding(1);
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

    void Move()
    {
        // Calculate movement
        float step = playerMovement.moveSpeed * Time.deltaTime;

        if (!GameStateManager.Instance.secondTrip)
        {
            transform.Translate(Vector2.right * step);
        }
        else
        {
            transform.Translate(Vector2.left * step);
        }
    }
}
