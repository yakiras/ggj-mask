using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenuGroup;
    public GameObject tutorialGroup;
    public string menuScene;
    public string gameScene;
    public string endScene;

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
}