using System.Collections;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class Ending : MonoBehaviour
{
    public Canvas canvas;
    public float fps = 3.0f;
    public Sprite[] ending0; // alone & poor
    public Sprite[] ending1; // alone & prison
    public Sprite[] ending2; // bro & prison
    public Sprite[] ending3OP;
    public Sprite[] ending3; // fucking dead
    public Sprite[] ending4; // rich

    private float frameRate; // seconds per frame

    private SpriteRenderer sr;
    private Sprite[] currentAnimation;
    private int currentFrame;
    private float timer;

    private int ending;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        frameRate = 1.0f / fps;
        canvas.enabled = false;
        ending = 1;

        //switch (GameStateManager.Instance.currentEnding)
        switch (ending)
        {
            case 0:
                SetAnimation(ending0);
                break;
            case 1:
                SetAnimation(ending1);
                break;
            case 2:
                SetAnimation(ending2);
                break;
            case 3:
                currentAnimation = ending3;
                PlayAnimationOnce(ending3OP);
                break;
            case 4:
                SetAnimation(ending4);
                break;
            default:
                break;
        }

        StartCoroutine(WaitAndDisplayUI());
    }

    void Update()
    {
        if (currentAnimation == null || currentAnimation.Length == 0) return;

        timer += Time.deltaTime;
        if (timer >= frameRate)
        {
            timer = 0f;
            currentFrame = (currentFrame + 1) % currentAnimation.Length;
            sr.sprite = currentAnimation[currentFrame];
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

    public void PlayAnimationOnce(Sprite[] anim)
    {
        StartCoroutine(PlayOnceCoroutine(anim));
    }

    private IEnumerator PlayOnceCoroutine(Sprite[] anim)
    {
        if (anim == null || anim.Length == 0) yield break;

        for (int i = 0; i < anim.Length; i++)
        {
            sr.sprite = anim[i];
            yield return new WaitForSeconds(frameRate);
        }

        SetAnimation(currentAnimation);
    }

    IEnumerator WaitAndDisplayUI()
    {
        yield return new WaitForSeconds(3f);
        canvas.enabled = true;
    }
}
