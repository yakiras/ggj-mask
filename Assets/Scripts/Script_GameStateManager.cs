using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    // This allows any script to type GameStateManager.Instance to get this data
    public static GameStateManager Instance;

    [Header("NPC Flags")]
    public bool talkedToGuard = false;

    [Header("Ending States")]
    public bool ENDING1 = false;

    private void Awake()
    {
        // Ensures only one manager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keeps data alive when changing scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }
}