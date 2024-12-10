using System.Collections.Generic;
using UnityEngine;

public class RiddlePanelMusicController : MonoBehaviour
{
    [System.Serializable]
    public class PanelMusic
    {
        public GameObject panel; // The panel GameObject
        public AudioSource musicSource; // The music AudioSource for this panel
    }

    public List<PanelMusic> panelsWithMusic; // List of panels and their respective music
    public AudioSource backgroundMusicSource; // AudioSource for background music

    private PanelMusic activePanelMusic = null; // Tracks the currently active panel music

    private void Update()
    {
        // Check each panel's active state
        foreach (var panelMusic in panelsWithMusic)
        {
            if (panelMusic.panel.activeSelf && activePanelMusic != panelMusic)
            {
                // A new panel is active
                SwitchToPanelMusic(panelMusic);
                return;
            }
        }

        // If no panels are active, switch back to background music
        if (activePanelMusic != null && !activePanelMusic.panel.activeSelf)
        {
            SwitchToBackgroundMusic();
        }
    }

    private void SwitchToPanelMusic(PanelMusic panelMusic)
    {
        // Stop the previous panel's music
        if (activePanelMusic != null && activePanelMusic.musicSource.isPlaying)
        {
            activePanelMusic.musicSource.Stop();
        }

        // Pause the background music
        if (backgroundMusicSource.isPlaying)
        {
            backgroundMusicSource.Stop();
        }

        // Play the new panel's music
        panelMusic.musicSource.Play();
        activePanelMusic = panelMusic;

        Debug.Log($"Switched to music for panel: {panelMusic.panel.name}");
    }

    private void SwitchToBackgroundMusic()
    {
        // Stop the active panel's music
        if (activePanelMusic != null && activePanelMusic.musicSource.isPlaying)
        {
            activePanelMusic.musicSource.Stop();
        }

        // Resume the background music
        backgroundMusicSource.Play();
        activePanelMusic = null;

        Debug.Log("Switched to background music.");
    }
}