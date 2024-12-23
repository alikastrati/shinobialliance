using UnityEngine;

public class AudioController : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.Play(); // Starts playing on start
        }
    }

    public void StopMusic()
    {
        if (audioSource != null)
        {
            audioSource.Stop(); // Stops the audio
        }
    }

    public void PlayMusic()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play(); // Starts playing the audio
        }
    }
}
