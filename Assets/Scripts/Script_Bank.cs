using UnityEngine;

public class Bank : MonoBehaviour
{
    public JewelryBoss boss;
    public PoliceStation station;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            boss.Flip();
            station.JailThug();

            if (!boss.thug)
            {
                boss.ReturnToShop();
            }
            else
            {
                if (GameStateManager.Instance.shopRobbed)
                {
                    GameStateManager.Instance.hasKey = false;
                    boss.SpawnPolice();
                    station.RemovePolicemen();
                }
                else
                {
                    boss.SpawnBodyguard();
                }
            }
        }
    }
}
