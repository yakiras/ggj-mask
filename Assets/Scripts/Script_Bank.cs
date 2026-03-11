using UnityEngine;

// This class evaluates & changes environment visuals/status
public class Bank : MonoBehaviour
{
    public JewelryBoss boss;
    public PoliceStation station;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //boss.Flip();
            station.JailThug();

            if (GameStateManager.Instance.alertLevel == 0)
            {
                station.GatherPolicemen();
                boss.ReturnToShop();
            }
            if (GameStateManager.Instance.alertLevel == 1)
            {
                station.AwakenPolice();
                boss.SpawnBodyguard();
            }
            if (GameStateManager.Instance.alertLevel == 2)
            {
                boss.SpawnPolice();
                station.RemovePolicemen();
            }
        }
    }
}
