using UnityEngine;

public class TestAudio : MonoBehaviour
{
    //�����ļ�
    public AudioSource []  hintvoice;


    /// <summary>���ŷ�����</summary>
    public void playMusic(int i)
    {
        if (hintvoice[i - 1] != null && !hintvoice[i - 1].isPlaying)
        {
            hintvoice[i - 1].Play();
        }
    }

    /// <summary>�ر����ֲ���</summary>
    public void stopMusic(int i)
    {
        if (hintvoice[i - 1] != null && !hintvoice[i - 1].isPlaying)
        {
            hintvoice[i - 1].Stop();
        }
    }

    /// <summary>��ͣ���ֲ���</summary>
    public void pauseMusic(int i)
    {
        if (hintvoice[i - 1] != null && !hintvoice[i - 1].isPlaying)
        {
            hintvoice[i - 1].Pause();
        }
    }

    /// <summary>
    /// ���ò�������
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
