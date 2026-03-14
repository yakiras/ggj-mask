using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioHelper : MonoBehaviour
{
    private AudioSource audioSource;
    private Coroutine loopRoutine;

    // Tracks clips currently playing on this object
    private HashSet<AudioClip> currentlyPlaying = new HashSet<AudioClip>();

    void Awake()
    {
        // Get the AudioSource on this GameObject
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void PlayLoop(AudioClip clip, float volume = 1f)
    {
        if (clip == null) return;

        // Prevent starting multiple loops
        if (loopRoutine != null)
            return;

        loopRoutine = StartCoroutine(LoopRoutine(clip, volume));
    }

    private IEnumerator LoopRoutine(AudioClip clip, float volume)
    {
        while (true)
        {
            audioSource.PlayOneShot(clip, volume);
            yield return new WaitForSeconds(clip.length);
        }
    }

    public void PlaySingle(AudioClip clip, float volume = 1f)
    {
        if (clip == null) return;

        // Stop any looping coroutine
        if (loopRoutine != null)
        {
            StopCoroutine(loopRoutine);
            loopRoutine = null;
        }

        audioSource.Stop();        // stop anything currently playing
        audioSource.loop = false;  // ensure no looping
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();
    }



    public void StopLoop()
    {
        if (loopRoutine != null)
        {
            StopCoroutine(loopRoutine);
            loopRoutine = null;
        }
    }

    public void StopPlaying()
    {
        audioSource.Stop();
    }

    /// <summary>
    /// Plays the clip once, ignoring repeated calls while it's playing.
    /// Works independently per GameObject.
    /// </summary>
    public void PlayOnce(AudioClip clip, float volume = 1f)
    {
        if (clip == null) return;

        // If the clip is already playing on this object, skip
        if (currentlyPlaying.Contains(clip))
            return;

        // Play and track the clip
        currentlyPlaying.Add(clip);
        audioSource.PlayOneShot(clip, volume);

        // Remove from tracking after the clip finishes
        StartCoroutine(RemoveClipAfterDuration(clip, clip.length));
    }

    private IEnumerator RemoveClipAfterDuration(AudioClip clip, float duration)
    {
        yield return new WaitForSeconds(duration);
        currentlyPlaying.Remove(clip);
    }
}