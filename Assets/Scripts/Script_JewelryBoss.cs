using UnityEngine;
using UnityEngine.InputSystem;

public class JewelryBoss : MonoBehaviour
{
    public PlayerDisguise playerController;

    public Sprite[] idle;
    public Sprite[] hearts;
    public Sprite[] shock;
    public Sprite[] sad;
    public Sprite[] readyGun;
    public Sprite[] shootGun;

    public bool thief = false;
    public bool girl = false;
    public bool thug = false;
    private float frameRate = 0.5f; // seconds per frame

    private SpriteRenderer sr;
    private Sprite[] currentAnimation;
    private int currentFrame;
    private float timer;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        SetAnimation(idle); // default animation

        // disable policemen
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (playerController.transform.position.x > transform.position.x)
                sr.flipX = true;
            else sr.flipX = false;

            if (!GameStateManager.Instance.secondTrip) CheckDisguiseR1();
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
                GameStateManager.Instance.hasKey = true;
                break;
        }
    }

    void CheckDisguiseR2()
    {
        switch (playerController.currentDisguise)
        {
            case "thief":
                SetAnimation(readyGun);
                // ENDING 2: bro & prison
                StartCoroutine(GameStateManager.Instance.DisplayEnding(2));
                break;
            case "girl":
                if (!GameStateManager.Instance.shopRobbed)
                    SetAnimation(hearts);
                break;
            case "thug":
                if (girl)
                {
                    SetAnimation(shootGun);
                    // ENDING 3: fucking dies
                    StartCoroutine(GameStateManager.Instance.DisplayEnding(3));
                }
                else
                {
                    StartCoroutine(GameStateManager.Instance.DisplayEnding(2));
                }
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

    public void ReturnToShop()
    {
        Vector3 pos = transform.localPosition;
        pos.x = 0f;
        transform.localPosition = pos;
        sr.sortingLayerName = "NPC(inside)";
        SetAnimation(idle);
    }

    public void SpawnPolice()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
        SetAnimation(idle);
    }
    public void SpawnBodyguard()
    {
        ReturnToShop();
        SetAnimation(idle);
        // todo: need sprite
    }

    public void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
