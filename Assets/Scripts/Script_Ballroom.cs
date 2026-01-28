using UnityEngine;

public class Ballroom : MonoBehaviour
{
    public PlayerDisguise playerController;
    public GameStateManager gameStateManager;

    public bool thief = false;
    public bool girl = false;
    public bool thug = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CheckDisguiseR1();
        }
    }
    void CheckDisguiseR1()
    {
        switch (playerController.currentDisguise)
        {
            case "thief":
                thief = true;
                // people shocked
                gameStateManager.money += 10;
                break;
            case "girl":
                girl = true;
                // do dance animation
                break;
            case "thug":
                thug = true;
                // people run away
                break;
        }
    }

}
