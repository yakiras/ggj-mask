using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    public GameObject mainMenuGroup;
    public GameObject tutorialGroup;
    public GameObject BGMHandler;
    public AudioClip bgmNormal;
    public AudioClip bgmHappy;
    public AudioClip bgmSad;
    public int money = 0;
    public bool hasKey = false;
    public bool shopRobbed = false;
    public string menuScene;
    public string gameScene;
    public string endScene;

    public Sprite ending1;

    private SpriteRenderer sr;
    private AudioSource source;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        source = GetComponent<AudioSource>();
    }

    public void StartTutorial()
    {
        if (mainMenuGroup != null)
        {
            mainMenuGroup.SetActive(false);
        }

        if (tutorialGroup != null)
        {
            tutorialGroup.SetActive(true);
        }

        Debug.Log("Switched to Tutorial");
    }

    //public void BackToMainMenu()
    //{
    //    if (mainMenuGroup != null)
    //    {
    //        mainMenuGroup.SetActive(true);
    //    }

    //    if (tutorialGroup != null)
    //    {
    //        tutorialGroup.SetActive(false);
    //    }

    //    Debug.Log("Switched to Main Menu");
    //}

    public void StartGame()
    {
        SceneManager.LoadScene(gameScene);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(menuScene);
        money = 0;
    }

    public IEnumerator DisplayEnding(int endingNum)
    {
        yield return new WaitForSeconds(1.5f);
        switch (endingNum)
        {
            case 1:
                // switch current ending
                break;
            case 2:
                break;
        }

        //SceneManager.LoadScene(endScene);
    }

    public IEnumerator StopBGM()
    {
        float startVolume = source.volume;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / 1.0f;
            source.volume = Mathf.Lerp(startVolume, 0f, t);
            yield return null;
        }

        source.Stop();
        source.volume = startVolume; // reset for next time
    }
    public IEnumerator StartBGM(string type)
    {
        switch (type)
        {
            case "normal":
                source.clip = bgmNormal;
                break;
            case "happy":
                source.clip = bgmHappy;
                break;
            case "sad":
                source.clip = bgmSad;
                break;
        }
        source.volume = 0f;
        source.Play();

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / 1.0f;
            source.volume = Mathf.Lerp(0f, 0.5f, t);
            yield return null;
        }
    }
}