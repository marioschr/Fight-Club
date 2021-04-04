using Michsky.UI.ModernUIPack;
using UnityEngine;

public class MusicVolume : MonoBehaviour // ορίζουμε την ένταση της μουσικής που επέλεξε ο χρήστης
{
    public SliderManager sliderMusic,sliderCrowd;
    private AudioSource audios;
    void Start()
    {
        audios = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();
    }


    public void SetMusicVolumeSlider()
    {
        sliderMusic.mainSlider.value = PlayerPrefs.GetFloat("MusicVolume",0.6f) * 100;
    }

    public void SetMusicVolume(float volume)
    {
        audios = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();
        audios.volume = volume / 100f;
        PlayerPrefs.SetFloat("MusicVolume",audios.volume);
    }
    
    public void SetCrowdVolumeSlider()
    {
        sliderCrowd.mainSlider.value = PlayerPrefs.GetFloat("CrowdVolume",0.6f) * 100;
    }

    public void SetCrowdVolume(float volume)
    {
        PlayerPrefs.SetFloat("CrowdVolume", volume / 100f);
    }
}
