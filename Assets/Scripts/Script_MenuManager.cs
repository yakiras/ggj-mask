using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    public GameObject mainMenuGroup;
    public GameObject tutorialGroup;
    public GameObject creditsGroup;
    public string menuScene;
    public string gameScene;
    public string endScene;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // remove duplicates
        }
    }

    public void StartTutorial()
    {
        if (mainMenuGroup != null)
        {
            mainMenuGroup.SetActive(false);
        }
        if (creditsGroup != null)
        {
            creditsGroup.SetActive(false);
        }

        if (tutorialGroup != null)
        {
            tutorialGroup.SetActive(true);
        }

        Debug.Log("Switched to Tutorial");
    }

    public void BackToMainMenu()
    {
        if (mainMenuGroup != null)
        {
            mainMenuGroup.SetActive(true);
        }

        if (tutorialGroup != null)
        {
            tutorialGroup.SetActive(false);
        }
        if (creditsGroup != null)
        {
            creditsGroup.SetActive(false);
        }

        Debug.Log("Switched to Main Menu");
    }

    public void StartCredits()
    {
        if (mainMenuGroup != null)
        {
            mainMenuGroup.SetActive(false);
        }
        if (tutorialGroup != null)
        {
            tutorialGroup.SetActive(false);
        }

        if (creditsGroup != null)
        {
            creditsGroup.SetActive(true);
        }

        Debug.Log("Switched to Credits");
    }

    public void StartGame()
    {
        SceneManager.LoadScene(gameScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public IEnumerator DisplayEnding()
    {
        // stop player
        GameStateManager.Instance.stopMoving = true;

        yield return new WaitForSeconds(1.5f);

        SceneManager.LoadScene(endScene);
    }
}