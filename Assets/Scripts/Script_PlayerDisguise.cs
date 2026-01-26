using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDisguise : MonoBehaviour
{
    public Sprite sprThief;
    public Sprite sprGirl;
    public Sprite sprThug;

    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Keyboard.current.digit1Key.wasPressedThisFrame)
            spriteRenderer.sprite = sprThief;

        if (Keyboard.current.digit2Key.wasPressedThisFrame)
                spriteRenderer.sprite = sprGirl;

        if (Keyboard.current.digit3Key.wasPressedThisFrame)
            spriteRenderer.sprite = sprThug;
    }
}
