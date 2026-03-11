using UnityEngine;

public class BuildingFade : MonoBehaviour
{
    public PlayerDisguise playerController;
    public SpriteRenderer topSprite;   // assign the top sprite
    public float fadeSpeed = 2f;       // how fast it fades
    public bool isJewelryShop = false;
    public bool isBallroom = false;
    public bool isBar = false;

    private bool playerNear = false;
    private bool ballroomLocked = false;
    //private bool isStealing = false;

    void Update()
    {
        if (topSprite == null) return;

        Color color = topSprite.color;

        if (playerNear)
        {
            // Fade out
            color.a = Mathf.MoveTowards(color.a, 0f, fadeSpeed * Time.deltaTime);
        }
        else
        {
            // Fade back in
            color.a = Mathf.MoveTowards(color.a, 1f, fadeSpeed * Time.deltaTime);
        }

        topSprite.color = color;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (isJewelryShop)
            {
                if (GameStateManager.Instance.hasKey ||
                    GameStateManager.Instance.secondTrip)
                {
                    playerNear = true;
                }
            }
            else if (isBallroom)
            {
                if (!ballroomLocked) playerNear = true;
            }
            else { playerNear = true; }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (playerController.currentDisguise == "thief")
            {
                if (isBallroom && !GameStateManager.Instance.ballroomRobbed)
                {
                    GameStateManager.Instance.money += 25;
                    GameStateManager.Instance.ballroomRobbed = true;
                    playerController.Steal();
                }
                else if (isBar && !GameStateManager.Instance.barRobbed)
                {
                    GameStateManager.Instance.money += 75;
                    GameStateManager.Instance.barRobbed = true;
                    playerController.Steal();
                }
                else if (isJewelryShop)
                {
                    if (GameStateManager.Instance.hasKey &&
                        !GameStateManager.Instance.shopRobbed)
                    {
                        GameStateManager.Instance.shopRobbed = true;
                        GameStateManager.Instance.alertLevel = 2;
                        GameStateManager.Instance.money += 100;
                        playerController.Steal();
                    }
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = false;

            if (playerController.currentDisguise.Equals("thief"))
                playerController.DefaultThiefAnimation();

            if (isBallroom && !GameStateManager.Instance.ballroomRobbed)
                ballroomLocked = true;
        }
    }
}
