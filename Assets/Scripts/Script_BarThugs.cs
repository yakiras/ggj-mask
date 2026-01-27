using UnityEngine;

public class BarThug : MonoBehaviour
{
    public PlayerDisguise playerController;
    public GameStateManager gameStateManager;

    public Sprite[] thugDrink;

    public bool thief = false;
    public bool girl = false;
    public bool thug = false;
    private bool secondRound = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("hit trigger");
        if (other.CompareTag("Player"))
        {
            if (!secondRound) CheckDisguiseR1();
            else CheckDisguiseR2();
        }
    }
    void CheckDisguiseR1()
    {
        switch (playerController.currentDisguise)
        {
            case "thief":
                // ENDING 1: get beat up by thug
                gameStateManager.DisplayEnding(1);
                break;
            case "girl":
                girl = true;
                // stalked by thug, thug beats up police
                break;
            case "thug":
                thug = true;
                StartCoroutine(playerController.SetAnimationWithDelay(thugDrink, 3.0f));
                break;
        }
        secondRound = true;
    }

    void CheckDisguiseR2()
    {
        switch (playerController.currentDisguise)
        {
            case "thief":
                thief = true;
                break;
            case "girl":
                girl = true;
                break;
            case "thug":
                thug = true;
                break;
        }
    }
}
