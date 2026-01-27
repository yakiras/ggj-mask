using System.Data.SqlTypes;
using UnityEngine;

public class Bank : MonoBehaviour
{
    public int moneyThreshold = 100;
    public GameStateManager gameStateManager;
    public JewelryBoss boss;
    public GameObject shop;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (gameStateManager.money < moneyThreshold)
            {
                StartCoroutine(gameStateManager.StartBGM("sad"));
            }
            else
            {
                StartCoroutine(gameStateManager.StartBGM("happy"));
            }

            boss.Flip();

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
                }
                else
                {
                    boss.SpawnBodyguard();
                }
            }
        }
    }
}
