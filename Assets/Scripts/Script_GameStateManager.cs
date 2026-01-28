using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;

    public int money = 0;
    public int moneyThreshold = 100;
    public bool hasKey = false;
    public bool shopRobbed = false;
    public string menuScene;
    public string gameScene;
    public string endScene;
    public bool secondTrip = false;
    public bool stopMoving = false;
    public bool atBankers = false;

    public int currentEnding = 0;

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

    public void BackToMenu()
    {
        SceneManager.LoadScene(menuScene);
        money = 0;
    }

    public IEnumerator DisplayEnding(int endingNum)
    {
        currentEnding = endingNum;
        // stop player
        stopMoving = true;
        yield return new WaitForSeconds(1.5f);
        
        SceneManager.LoadScene(endScene);
    }

    public void ResetGame()
    {
        money = 0;
        hasKey = false;
        shopRobbed = false;
        secondTrip = false;
        stopMoving = false;
        atBankers = false;

        currentEnding = 0;
    }
}