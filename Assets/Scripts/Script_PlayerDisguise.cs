using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDisguise : MonoBehaviour
{
    public Sprite sprThief;
    public Sprite sprGirl;
    public Sprite sprThug;

    private SpriteRenderer spriteRenderer;
    private Animator animator;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Keyboard.current.digit1Key.wasPressedThisFrame)
            animator.Play("Anim_Thief_Walk");

        if (Keyboard.current.digit2Key.wasPressedThisFrame)
            spriteRenderer.sprite = sprGirl;

        if (Keyboard.current.digit3Key.wasPressedThisFrame)
            spriteRenderer.sprite = sprThug;
    }
}
