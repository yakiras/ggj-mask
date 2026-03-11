using System.Collections;
using UnityEngine;

public class Banker : MonoBehaviour
{
    public BGMManager bgmManager;
    public PlayerDisguise player;

    public Sprite[] idle;
    public Sprite[] yes;
    public Sprite[] no;

    public float frameRate = 1.5f;

    private SpriteRenderer sr;
    private Sprite[] currentAnimation;
    private int currentFrame;
    private float timer;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        SetAnimation(idle);
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(EvaluatePlayer());
        }
    }

    IEnumerator EvaluatePlayer()
    {
        GameStateManager.Instance.atBankers = true;
        GameStateManager.Instance.stopMoving = true;


        Debug.Log(GameStateManager.Instance.money);
        if (GameStateManager.Instance.money < GameStateManager.Instance.moneyThreshold)
        {
            StartCoroutine(bgmManager.SwitchBGM("sad"));
            SetAnimation(no);
        }
        else
        {
            StartCoroutine(bgmManager.SwitchBGM("happy"));
            SetAnimation(yes);

        }
        yield return new WaitForSeconds(4.0f);

        GameStateManager.Instance.atBankers = false;
        GameStateManager.Instance.stopMoving = false;
        GameStateManager.Instance.secondTrip = true;

        player.DefaultThiefAnimation();
        player.FlipSprite();

    }

    public void SetAnimation(Sprite[] newAnimation)
    {
        if (newAnimation == currentAnimation) return;
        currentAnimation = newAnimation;
        currentFrame = 0;
        timer = 0f;
        if (currentAnimation.Length > 0)
            sr.sprite = currentAnimation[0];
    }
}
