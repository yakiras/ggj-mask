using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    public Image uiOverlay;
    public Image uiIcon;

    public Sprite thiefIcon;
    public Sprite girlIcon;
    public Sprite thugIcon;

    float cooldownDuration;
    float timer;
    bool coolingDown;

    private void Start()
    {
        uiOverlay.fillAmount = 0f;
    }

    void Update()
    {
        if (!coolingDown) return;

        timer -= Time.deltaTime;

        uiOverlay.fillAmount = timer / cooldownDuration;

        if (timer <= 0)
        {
            coolingDown = false;
            uiOverlay.fillAmount = 0f;
        }
    }

    public void SwapIcon(string disguise)
    {
        if (disguise == "thief")
        {
            uiOverlay.sprite = thiefIcon;
            uiIcon.sprite = thiefIcon;
        }
        if (disguise == "girl")
        {
            uiOverlay.sprite = girlIcon;
            uiIcon.sprite = girlIcon;
        }
        if (disguise == "thug")
        {
            uiOverlay.sprite = thugIcon;
            uiIcon.sprite = thugIcon;
        }
    }

    public void DisplayCooldown(float duration)
    {
        cooldownDuration = duration;
        timer = duration;
        coolingDown = true;

        uiOverlay.fillAmount = 1f;
    }
}