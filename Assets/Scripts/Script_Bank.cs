using UnityEngine;

public class Bank : MonoBehaviour
{
    public int moneyThreshold = 100;
    public BGMManager bgmManager;
    public JewelryBoss boss;
    public PoliceStation station;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log(GameStateManager.Instance.money);
            if (GameStateManager.Instance.money < moneyThreshold)
            {
                StartCoroutine(bgmManager.SwitchBGM("sad"));
            }
            else
            {
                StartCoroutine(bgmManager.SwitchBGM("happy"));
            }

            boss.Flip();
            station.JailThug();
            GameStateManager.Instance.secondTrip = true;

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
