using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenuGroup;
    public GameObject tutorialGroup;
    public GameObject BGMHandler;
    public string menuScene;
    public string gameScene;
    public string endScene;

    private AudioSource source;

    private void Start()
    {
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
}