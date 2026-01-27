using UnityEngine;

public class Ballroom : MonoBehaviour
{
    public PlayerDisguise playerController;
    public bool thief = false;
    public bool girl = false;
    public bool thug = false;
    private bool secondRound = false;

    void OnTriggerEnter2D(Collider2D other)
    {
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
                thief = true;
                break;
            case "girl":
                girl = true;
                break;
            case "thug":
                thug = true;
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
