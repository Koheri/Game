using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour
{
    public AudioMixer audioMix;
    private int  quality = 1;
    private float volum = (float)0.5;
    public Slider slider_;
    public Dropdown dropdown_;
    public Toggle togScreen, togMusic;

    private void Start()
    {
        LoadData();
    }
    public void SetVolume(float volume)
    {
        slider_.value = volum = volume;
        audioMix.SetFloat("volume", Mathf.Log10(volum) * 20);
    }
    public void SetQuality(int qualityIndex)
    {
        dropdown_.value = quality = qualityIndex;
        QualitySettings.SetQualityLevel(quality+1);
    }
    public void Sound()
    {
        if (togMusic.isOn == true) AudioListener.pause = false;
        else AudioListener.pause = true;
    }
    public void SetFullScreen (bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
    public void SaveData()
    {
        PlayerPrefs.SetInt("FullScreenPrefs", System.Convert.ToInt32(Screen.fullScreen));
        PlayerPrefs.SetInt("ScreenPref", System.Convert.ToInt32(togScreen.isOn));
        PlayerPrefs.SetInt("MusicPausePref", System.Convert.ToInt32(AudioListener.pause));
        PlayerPrefs.SetInt("MusicPref", System.Convert.ToInt32(togMusic.isOn));
        PlayerPrefs.SetFloat("MusicVolumPref", volum);
        PlayerPrefs.SetInt("QualityPref", quality);
    }
    private void LoadData()
    {
        if (PlayerPrefs.HasKey("FullScreenPrefs")) Screen.fullScreen = System.Convert.ToBoolean(PlayerPrefs.GetInt("FullScreenPrefs"));
        else Screen.fullScreen = true;
        if (PlayerPrefs.HasKey("ScreenPref")) togScreen.isOn = System.Convert.ToBoolean(PlayerPrefs.GetInt("ScreenPref"));
        if (PlayerPrefs.HasKey("MusicPausePref")) AudioListener.pause = System.Convert.ToBoolean(PlayerPrefs.GetInt("MusicPausePref"));
        else AudioListener.pause = false;
        if (PlayerPrefs.HasKey("MusicPref")) togMusic.isOn = System.Convert.ToBoolean(PlayerPrefs.GetInt("MusicPref"));
        if (PlayerPrefs.HasKey("MusicVolumPref")) slider_.value = volum = PlayerPrefs.GetFloat("MusicVolumPref");
        else slider_.value = volum = (float)0.5;
        if (PlayerPrefs.HasKey("QualityPref")) {quality = PlayerPrefs.GetInt("QualityPref");dropdown_.value = quality; }
        else { quality  = 1; dropdown_.value = quality; }
    }
}
