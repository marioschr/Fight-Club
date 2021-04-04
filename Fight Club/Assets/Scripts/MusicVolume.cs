using Michsky.UI.ModernUIPack;
using UnityEngine;

public class MusicVolume : MonoBehaviour // ορίζουμε την ένταση της μουσικής που επέλεξε ο χρήστης
{
    public SliderManager slider;
    private AudioSource audios;
    void Start()
    {
        audios = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();
    }


    public void SetMusicVolumeSlider()
    {
        slider.mainSlider.value = PlayerPrefs.GetFloat("MusicVolume",0.6f) * 100;
    }

    public void SetMusicVolume(float volume)
    {
        audios = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();
        audios.volume = volume / 100f;
        PlayerPrefs.SetFloat("MusicVolume",audios.volume);
    }
}
