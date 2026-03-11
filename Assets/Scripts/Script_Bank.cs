using UnityEngine;

// This class evaluates & changes environment visuals/status
public class Bank : MonoBehaviour
{
    public GameObject barThug;
    public JewelryBoss boss;
    public PoliceStation station;
    public Ballroom ballroom;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            station.JailThug();
            barThug.SetActive(false);

            if (GameStateManager.Instance.alertLevel == 0)
            {
                station.GatherPolicemen();
                boss.ReturnToShop();
            }
            if (GameStateManager.Instance.alertLevel == 1)
            {
                station.RemoveSleepingPolice();
                boss.SpawnBodyguard();
            }
            if (GameStateManager.Instance.alertLevel == 2)
            {
                boss.SpawnPolice();
                station.RemovePolicemen();
            }
            if (GameStateManager.Instance.ballroomRobbed)
                ballroom.SpawnPolice();
        }
    }
}
