using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class JewelryBoss : MonoBehaviour
{
    public PlayerDisguise playerController;

    public AudioClip sfxHearts;
    public AudioClip sfxGun;
    public AudioClip sfxSad;
    public AudioClip sfxGasp;
    public AudioClip sfxConfused;
    public AudioClip sfxSiren;

    public GameObject[] policemen;
    public GameObject bodyguard;

    public Sprite[] idle;
    public Sprite[] hearts;
    public Sprite[] shock;
    public Sprite[] sad;
    public Sprite[] readyGun;
    public Sprite[] shootGun;
    public Sprite[] keyDrop;
    public Sprite[] keyMissing;

    private float frameRate = 0.4f; // seconds per frame

    private SpriteRenderer sr;
    private Sprite[] currentAnimation;
    private int currentFrame;
    private float timer;
    private bool playOnce = false;
    private AudioHelper audioHelper;

    void Start()
    {
        audioHelper = GetComponent<AudioHelper>();
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
        if (currentAnimation == null || currentAnimation.Length == 0) return;

        timer += Time.deltaTime;
        if (timer >= frameRate)
        {
            timer = 0f;

            // PLAY ONCE MODE - only for key drop
            if (playOnce)
            {
                currentFrame++;

                if (currentFrame >= currentAnimation.Length)
                {
                    // switch animation
                    currentAnimation = keyMissing;
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (playerController.transform.position.x > transform.position.x)
                sr.flipX = true;
            else sr.flipX = false;

            if (!GameStateManager.Instance.secondTrip) CheckDisguise();
            else CheckDisguiseR2();
        }
    }

    void CheckDisguise()
    {
        switch (playerController.currentDisguise)
        {
            case "thief":
                GameStateManager.Instance.alertLevel = 1;
                SetAnimation(shock);
                audioHelper.PlaySingle(sfxGasp);
                break;
            case "girl":
                // alert level stays at 0
                SetAnimation(hearts);
                audioHelper.PlaySingle(sfxHearts);
                break;
            case "thug":
                GameStateManager.Instance.alertLevel = 1;
                playOnce = true;
                SetAnimation(keyDrop);
                GameStateManager.Instance.hasKey = true;
                audioHelper.PlaySingle(sfxConfused);
                break;
        }
    }

    void CheckDisguiseR2()
    {
        if (playerController.currentDisguise.Equals("girl"))
        {
            if (!GameStateManager.Instance.shopRobbed)
            {
                SetAnimation(hearts);
                audioHelper.PlaySingle(sfxHearts);
            }
        }
        else // thief or thug
        {
            if (GameStateManager.Instance.alertLevel == 0)
            {
                if (playerController.currentDisguise.Equals("thief"))
                {
                    // ENDING 4: bro & prison
                    SetAnimation(readyGun);
                    audioHelper.PlaySingle(sfxGun);
                    GameStateManager.Instance.InitializeEnding(4);
                }
                else
                {
                    // ENDING 3: fucking dead
                    SetAnimation(readyGun);
                    audioHelper.PlaySingle(sfxGun);
                    GameStateManager.Instance.InitializeEnding(3);
                }
            }
            if (GameStateManager.Instance.alertLevel == 1)
            {
                // ENDING 3: fucking dead
                SetAnimation(readyGun);
                audioHelper.PlaySingle(sfxGun);
                GameStateManager.Instance.InitializeEnding(3);
            }
            if (GameStateManager.Instance.alertLevel == 2)
            {
                // ENDING 4: bro & prison
                audioHelper.PlayOnce(sfxSiren);
                GameStateManager.Instance.InitializeEnding(4);
            }
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
        pos.y = -2.7f;
        transform.localPosition = pos;
        sr.sortingLayerName = "NPC(inside)";
        SetAnimation(idle);
    }

    public void SpawnBodyguard()
    {
        ReturnToShop();
        SetAnimation(idle);
        bodyguard.SetActive(true);
    }

    public void SpawnPolice()
    {
        foreach (GameObject police in policemen)
        {
            police.SetActive(true);
        }
        SetAnimation(sad);
        audioHelper.PlayLoop(sfxSad, 0.6f);
    }

    public void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
