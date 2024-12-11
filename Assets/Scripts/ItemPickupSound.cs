using UnityEngine;

public class ItemPickupSound : MonoBehaviour
{
    public AudioClip pickupSound; // Sound to play when the item is picked up
    private AudioSource audioSource;

    private void Start()
    {
        // Ensure the GameObject has an AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void PlaySpecificSound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning("AudioClip is null! Cannot play sound.");
        }
    }
}