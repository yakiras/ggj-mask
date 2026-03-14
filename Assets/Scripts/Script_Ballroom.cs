using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class Ballroom : MonoBehaviour
{
    public AudioClip sfxGasp;
    private AudioSource audioSource;

    public PlayerDisguise playerController;
    public Sprite[] animationFrames;
    public Transform people;
    public GameObject police;
    private float frameRate;
    private string knownDisguise;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        police.SetActive(false);
        frameRate = 0.5f;
        knownDisguise = string.Empty;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!GameStateManager.Instance.ballroomRobbed)
                audioSource.Stop();
            if (playerController.currentDisguise.Equals("girl"))
                playerController.DefaultGirlAnimation();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameStateManager.Instance.secondTrip &&
                GameStateManager.Instance.ballroomRobbed)
            {
                CheckDisguiseR2();
            }
            else { CheckDisguise(); }
        }
    }

    private void CheckDisguise()
    {
        if (knownDisguise != playerController.currentDisguise)
        {
            knownDisguise = playerController.currentDisguise;
            switch (playerController.currentDisguise)
            {
                case "thief":
                    AnimateChildrenWithRandomOffsets();
                    break;
                case "girl":
                    GameStateManager.Instance.stealAnimLocked = true;
                    GameStateManager.Instance.kissAnimLocked = true;
                    playerController.BlowKiss();
                    break;
                case "thug":
                    AnimateChildrenWithRandomOffsets();
                    break;
            }
        }
    }

    private void CheckDisguiseR2()
    {
        if (playerController.currentDisguise != "girl")
        {
            GameStateManager.Instance.InitializeEnding(4);
        }
    }

    public void LockBallroom()
    {
        audioSource.Stop();
    }

    public void SpawnPolice()
    {
        police.SetActive(true);
        audioSource.Stop();
    }

    public void AnimateChildrenWithRandomOffsets()
    {
        if (people != null)
        {
            foreach (Transform child in people)
            {
                audioSource.PlayOneShot(sfxGasp);

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
