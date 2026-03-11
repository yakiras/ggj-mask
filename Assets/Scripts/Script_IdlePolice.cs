using UnityEngine;

public class IdlePolice : MonoBehaviour
{
    // -3.83, 0
    public Sprite[] frames;
    public float stationLocation;
    public float frameRate = 0.15f;

    private SpriteRenderer sr;
    private int currentFrame;
    private float timer;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        // Start at a random frame + time
        currentFrame = Random.Range(0, frames.Length);
        timer = Random.Range(0f, frameRate);

        sr.sprite = frames[currentFrame];
    }

    void Update()
    {
        if (frames.Length == 0) return;

        timer += Time.deltaTime;
        if (timer >= frameRate)
        {
            timer = 0f;
            currentFrame = (currentFrame + 1) % frames.Length;
            sr.sprite = frames[currentFrame];
        }
    }

    public void ReturnToStation()
    {
        sr.enabled = true;
        Vector3 pos = transform.localPosition;
        pos.x = stationLocation;
        pos.y = -1.25f;
        transform.localPosition = pos;
        sr.sortingLayerName = "NPC(inside)";

        sr.sprite = frames[0];
        frames = new Sprite[0];
    }
}
