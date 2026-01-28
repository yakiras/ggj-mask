using System.Data.SqlTypes;
using UnityEngine;

public class Bank : MonoBehaviour
{
    public int moneyThreshold = 100;
    public GameStateManager gameStateManager;
    public JewelryBoss boss;
    public PoliceStation station;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log(gameStateManager.money);
            if (gameStateManager.money < moneyThreshold)
            {
                StartCoroutine(gameStateManager.StartBGM("sad"));
            }
            else
            {
                StartCoroutine(gameStateManager.StartBGM("happy"));
            }

            boss.Flip();
            station.JailThug();

            if (!boss.thug)
            {
                boss.ReturnToShop();
            }
            else
            {
                if (gameStateManager.shopRobbed)
                {
                    gameStateManager.hasKey = false;
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
