using UnityEngine;

// this is very janky kms
public class Script_BallroomPolice : MonoBehaviour
{
    public PlayerDisguise playerController;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CheckDisguise();
        }
    }

    private void CheckDisguise()
    {
        if (!playerController.currentDisguise.Equals("girl"))
        {
            GameStateManager.Instance.InitializeEnding(4);
        }
    }
}
