using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffSound : MonoBehaviour
{
    private bool isMuted;

    private void Start()
    {
        // Load the mute state from PlayerPrefs
        isMuted = PlayerPrefs.GetInt("IsMuted", 0) == 1;

        // Apply the mute state at the start of the scene
        ToggleAudioSources(isMuted);
    }

    private void Update()
    {
        // Check if the "M" key is pressed
        if (Input.GetKeyDown(KeyCode.M))
        {
            // Toggle mute state
            isMuted = !isMuted;

            // Save the mute state to PlayerPrefs
            PlayerPrefs.SetInt("IsMuted", isMuted ? 1 : 0);
            PlayerPrefs.Save();

            // Mute or unmute all AudioSources in the scene
            ToggleAudioSources(isMuted);
        }
    }

    private void ToggleAudioSources(bool mute)
    {
        // Get all AudioSources in the scene
        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();

        // Loop through each AudioSource and mute/unmute accordingly
        foreach (AudioSource audioSource in allAudioSources)
        {
            audioSource.mute = mute;
        }
    }
}