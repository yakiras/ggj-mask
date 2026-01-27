using UnityEngine;

public class BuildingFade : MonoBehaviour
{
    public GameStateManager gameStateManager;
    public PlayerDisguise playerController;
    public SpriteRenderer topSprite;   // assign the top sprite
    public float fadeSpeed = 2f;       // how fast it fades
    public bool isJewelryShop = false;
    private bool playerNear = false;

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
                if (gameStateManager.hasKey)
                {
                    playerNear = true;
                }
            }
            else { playerNear = true; }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!isJewelryShop) return;

        if (playerController.currentDisguise == "thief")
        {
            gameStateManager.money += 100;
            // play stealing animation
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = false;
        }
    }
}
