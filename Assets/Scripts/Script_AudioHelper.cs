using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioHelper : MonoBehaviour
{
    private AudioSource audioSource;
    private Coroutine sfxRoutine;

    // Tracks clips currently playing on this object
    private HashSet<AudioClip> currentlyPlaying = new HashSet<AudioClip>();

    void Awake()
    {
        // Get the AudioSource on this GameObject
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
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