using UnityEngine;

public class TestAudio : MonoBehaviour
{
    //“Ù¿÷Œƒº˛
    public AudioSource []  hintvoice;


    /// <summary>≤•∑≈∑≈“Ù¿÷</summary>
    public void playMusic(int i)
    {
        if (hintvoice[i - 1] != null && !hintvoice[i - 1].isPlaying)
        {
            hintvoice[i - 1].Play();
        }
    }

    /// <summary>πÿ±’“Ù¿÷≤•∑≈</summary>
    public void stopMusic(int i)
    {
        if (hintvoice[i - 1] != null && !hintvoice[i - 1].isPlaying)
        {
            hintvoice[i - 1].Stop();
        }
    }

    /// <summary>‘›Õ£“Ù¿÷≤•∑≈</summary>
    public void pauseMusic(int i)
    {
        if (hintvoice[i - 1] != null && !hintvoice[i - 1].isPlaying)
        {
            hintvoice[i - 1].Pause();
        }
    }

    /// <summary>
    /// …Ë÷√≤•∑≈“Ù¡ø
    /// </summary>
    /// <param name="volume"></param>
    public void setMusicVolume(int i, float volume)
    {
        if (hintvoice[i - 1] != null && !hintvoice[i - 1].isPlaying)
        {
            hintvoice[i - 1].volume = volume;
        }
    }
}
