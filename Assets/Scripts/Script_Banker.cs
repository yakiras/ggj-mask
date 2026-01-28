using System.Collections;
using UnityEngine;

public class Banker : MonoBehaviour
{
    public BGMManager bgmManager;

    public Sprite[] idle;
    public Sprite[] yes;
    public Sprite[] no;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            EvaluatePlayer();
        }
    }

    IEnumerator EvaluatePlayer()
    {
        GameStateManager.Instance.stopMoving = true;

        yield return new WaitForSeconds(2.0f);

        Debug.Log(GameStateManager.Instance.money);
        if (GameStateManager.Instance.money < GameStateManager.Instance.moneyThreshold)
        {
            StartCoroutine(bgmManager.SwitchBGM("sad"));
            //play no animation
        }
        else
        {
            StartCoroutine(bgmManager.SwitchBGM("happy"));
            //play yes animation

        }

        GameStateManager.Instance.stopMoving = false;
        GameStateManager.Instance.secondTrip = true;

    }

    // collider on touch
    // set stop moving
    // wait
    // yes/no
}
