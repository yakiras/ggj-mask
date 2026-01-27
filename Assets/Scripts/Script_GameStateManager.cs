using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    public GameObject mainMenuGroup;
    public GameObject tutorialGroup;
    public GameObject BGMHandler;
    public int money = 0;
    public bool hasKey = false;
    public string menuScene;
    public string gameScene;
    public string endScene;

    public Sprite ending1;

    private SpriteRenderer sr;
    private AudioSource audioSource;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
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

    public void DisplayEnding(int endingNum)
    {
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


    public void ChangeBGM()
    {

    }
}