using UnityEngine;

public class Bank : MonoBehaviour
{
    public PlayerDisguise playerController;
    public GameStateManager gameStateManager;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
        }
    }
}
