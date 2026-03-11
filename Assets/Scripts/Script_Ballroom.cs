using System.Collections;
using UnityEngine;

public class Ballroom : MonoBehaviour
{
    public PlayerDisguise playerController;
    public Sprite[] animationFrames;
    public Transform people;
    private float frameRate;

    private void Start()
    {
        frameRate = 0.5f;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (playerController.currentDisguise.Equals("girl"))
                playerController.DefaultGirlAnimation();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CheckDisguise();
        }
    }

    void CheckDisguise()
    {
        switch (playerController.currentDisguise)
        {
            case "thief":
                AnimateChildrenWithRandomOffsets();
                // add cha-ching sfx
                break;
            case "girl":
                playerController.BlowKiss();
                break;
            case "thug":
                AnimateChildrenWithRandomOffsets();
                break;
        }
    }

    public void AnimateChildrenWithRandomOffsets()
    {
        if (people != null)
        {
            foreach (Transform child in people)
            {
                SpriteRenderer sr = child.GetComponent<SpriteRenderer>();
                if (sr != null)
                {
                    StartCoroutine(AnimateChild(sr));
                }
            }
        }
    }

    IEnumerator AnimateChild(SpriteRenderer sr)
    {
        int frame = Random.Range(0, animationFrames.Length);
        float timer = Random.Range(0f, frameRate);

        while (true)
        {
            timer += Time.deltaTime;
            if (timer >= frameRate)
            {
                timer = 0f;
                frame = (frame + 1) % animationFrames.Length;
                sr.sprite = animationFrames[frame];
            }

            yield return null;
        }
    }

}
