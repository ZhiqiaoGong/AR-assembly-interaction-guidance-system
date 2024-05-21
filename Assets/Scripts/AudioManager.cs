using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AudioManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = "AudioManager";
                    instance = obj.AddComponent<AudioManager>();
                }
            }
            return instance;
        }
    }

    private AudioSource audioSource;
    // The fade speed
    private float fadeSpeed;

    private void Awake()
    {
        //audioSource = gameObject.AddComponent<AudioSource>();

        //// Set the initial volume to zero
        //audioSource.volume = 0f;

        //// Set the fade speed to 0.5f
        //fadeSpeed = 0.5f;
    }

    public void PlayAudio(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void PauseAudio()
    {
        audioSource.Pause();
    }

    public void StopAudio()
    {
        audioSource.Stop();
    }

    // A method to fade the audio source to a given volume in a given time
    public void FadeAudio(float targetVolume, float duration)
    {
        // Start a coroutine to fade the audio
        StartCoroutine(FadeAudioCoroutine(targetVolume, duration));
    }

    // A coroutine to fade the audio source to a given volume in a given time
    private IEnumerator FadeAudioCoroutine(float targetVolume, float duration)
    {
        // Get the current volume
        float currentVolume = audioSource.volume;

        // Calculate the elapsed time
        float elapsedTime = 0f;

        // While the elapsed time is less than the duration
        while (elapsedTime < duration)
        {
            // Update the elapsed time
            elapsedTime += Time.deltaTime;

            // Calculate the progress
            float progress = Mathf.Clamp01(elapsedTime / duration);

            // Lerp the volume between the current and target volumes based on the progress
            audioSource.volume = Mathf.Lerp(currentVolume, targetVolume, progress);

            // Yield until next frame
            yield return null;
        }

        // Set the volume to the target volume
        audioSource.volume = targetVolume;
    }
}