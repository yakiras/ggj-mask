using System.Collections;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public float fadeSpeed = 1.0f;

    public AudioClip bgmNormal;
    public AudioClip bgmHappy;
    public AudioClip bgmSad;

    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public IEnumerator SwitchBGM(string type)
    {
        yield return StartCoroutine(StopBGM());
        yield return StartCoroutine(StartBGM(type));
    }

    public IEnumerator StopBGM()
    {
        float startVolume = source.volume;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / fadeSpeed;
            source.volume = Mathf.Lerp(startVolume, 0f, t);
            yield return null;
        }

        source.Stop();
        source.volume = startVolume; // reset for next time
    }
    public IEnumerator StartBGM(string type)
    {
        switch (type)
        {
            case "normal":
                source.clip = bgmNormal;
                break;
            case "happy":
                source.clip = bgmHappy;
                break;
            case "sad":
                source.clip = bgmSad;
                break;
        }
        source.volume = 0f;
        source.Play();

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / fadeSpeed;
            source.volume = Mathf.Lerp(0f, 0.5f, t);
            yield return null;
        }
    }
}
