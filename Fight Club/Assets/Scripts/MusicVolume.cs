using System.Collections;
using System.Collections.Generic;
using Michsky.UI.ModernUIPack;
using UnityEngine;

public class MusicVolume : MonoBehaviour
{
    public SliderManager slider;
    private AudioSource audio;
    void Start()
    {
        audio = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();
    }


    public void SetMusicVolumeSlider()
    {
        slider.mainSlider.value = PlayerPrefs.GetFloat("MusicVolume",0.6f) * 100;
    }

    public void SetMusicVolume(float volume)
    {
        audio = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();
        audio.volume = volume / 100f;
        PlayerPrefs.SetFloat("MusicVolume",audio.volume);
    }
}
