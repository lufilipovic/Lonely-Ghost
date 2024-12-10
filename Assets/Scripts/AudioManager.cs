using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource backgroundMusicSource; // AudioSource for background music
    public AudioSource panelMusicSource; // AudioSource for panel-specific music

    private static AudioManager instance;

    private void Awake()
    {
        // Ensure only one instance exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keep across scenes
        }
        else
        {
            Destroy(gameObject); // Prevent duplicates
        }
    }

    public void PlayBackgroundMusic()
    {
        // Stop panel music if it's playing
        if (panelMusicSource.isPlaying)
        {
            panelMusicSource.Stop();
        }

        // Play background music if not already playing
        if (!backgroundMusicSource.isPlaying)
        {
            backgroundMusicSource.Play();
        }
    }

    public void PlayPanelMusic()
    {
        // Stop background music if it's playing
        if (backgroundMusicSource.isPlaying)
        {
            backgroundMusicSource.Stop();
        }

        // Play panel music if not already playing
        if (!panelMusicSource.isPlaying)
        {
            panelMusicSource.Play();
        }
    }

    public void StopAllMusic()
    {
        backgroundMusicSource.Stop();
        panelMusicSource.Stop();
    }
}