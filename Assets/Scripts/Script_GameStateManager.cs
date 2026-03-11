using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;

    public bool broFollowing = false;
    public int alertLevel = 0;

    public int money = 0;
    public int moneyThreshold = 100;
    public bool hasKey = false;
    public bool shopRobbed = false;
    public bool barRobbed = false;
    public bool ballroomRobbed = false;
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

    public void InitializeEnding(int endingNum)
    {
        currentEnding = endingNum;
        StartCoroutine(MenuManager.Instance.DisplayEnding());
    }

    public void EvaluateEnding()
    {
        if (money < moneyThreshold)
        {
            if (broFollowing)
                InitializeEnding(5);
            else
                InitializeEnding(1);
        }
        else
        {
            if (broFollowing)
                InitializeEnding(6);
            else
                InitializeEnding(2);
        }
    }

    public void ResetGame()
    {
        money = 0;
        broFollowing = false;
        alertLevel = 0;
        hasKey = false;
        shopRobbed = false;
        secondTrip = false;
        stopMoving = false;
        atBankers = false;

        currentEnding = 0;
    }
}