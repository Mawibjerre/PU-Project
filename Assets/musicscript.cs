using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    [Header("Music Playlist")]
    public AudioClip[] tracks;       // Drag in multiple songs here
    public bool loopSingle = false;  // If true, loops the same track forever
    public bool shuffle = false;     // If true, picks random tracks

    private AudioSource audioSource;
    private int currentTrack = 0;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false; // we handle looping manually
        PlayNextTrack();
    }

    void Update()
    {
        // When a track finishes, play the next one
        if (!audioSource.isPlaying)
        {
            PlayNextTrack();
        }
    }

    void PlayNextTrack()
    {
        if (tracks.Length == 0) return;

        if (loopSingle)
        {
            // Always play the same track
            audioSource.clip = tracks[currentTrack];
        }
        else if (shuffle)
        {
            // Pick a random track
            currentTrack = Random.Range(0, tracks.Length);
            audioSource.clip = tracks[currentTrack];
        }
        else
        {
            // Cycle through tracks in order
            audioSource.clip = tracks[currentTrack];
            currentTrack = (currentTrack + 1) % tracks.Length;
        }

        audioSource.Play();
    }
}