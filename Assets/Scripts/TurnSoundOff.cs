using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffSound : MonoBehaviour

{
    private bool isMuted = false;

    // Update is called once per frame
    void Update()
    {
        // Check if the "M" key is pressed
        if (Input.GetKeyDown(KeyCode.M))
        {
            // Toggle mute state
            isMuted = !isMuted;

            // Mute or unmute all AudioSources in the scene
            ToggleAudioSources(isMuted);
        }
    }

    void ToggleAudioSources(bool mute)
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