using System.Data.SqlTypes;
using UnityEngine;

public class Bank : MonoBehaviour
{
    public int moneyThreshold = 100;
    public GameStateManager gameStateManager;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (gameStateManager.money < moneyThreshold)
            {
                // sad mc
            }
            else
            {
                // vip
            }
        }
    }
}
