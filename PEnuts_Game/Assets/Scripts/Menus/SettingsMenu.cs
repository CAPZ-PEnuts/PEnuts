﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{

    public AudioMixer MainMixer;
    private AudioManager AudioManager;


    public string nameMasterVolume;
    public string nameMusicVolume;
    public string nameEffectVolume;

    private int _counter = 0;

    public Dropdown resolutionDropdown;

    Resolution[] resolutions;
    
    void Start()
    {
        resolutions = Screen.resolutions;
        AudioManager = FindObjectOfType<AudioManager>();

        var fullscreenToggle = GameObject.Find("FullscreenToggle").GetComponent<Toggle>();
        fullscreenToggle.isOn = Screen.fullScreen;

        if (resolutionDropdown.options != null)
            resolutionDropdown.ClearOptions();
        
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.width &&
                resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }
        
        if (options != null)
            resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

 
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    
    public void SetMasterVolume(float volume)
    {
        MainMixer.SetFloat(nameMasterVolume, volume);
    }
    
    public void SetMusicVolume(float volume)
    {
        MainMixer.SetFloat(nameMusicVolume, volume);
    }    
    
    public void SetEffectVolume(float volume)
    {
        MainMixer.SetFloat(nameEffectVolume, volume);
        AudioManager.setPitch(volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

}
