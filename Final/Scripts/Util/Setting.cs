using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    [SerializeField]FloatData musicVolume;
    [SerializeField]FloatData sfxVolume;

    [SerializeField] Slider musicslider;
    [SerializeField] Slider sfxSlider;

    public void Awake()
    {
        OnSFXVolumeChange(sfxVolume.Value);
        OnMusicVolumeChange(musicVolume.Value);
        musicslider.value = musicVolume.Value;
        sfxSlider.value = sfxVolume.Value;
    }
    public void OnSFXVolumeChange(float value)
    {
        AudioController.Instance.SetSFXVolume(value);
        sfxVolume.Value = value;
    }

    public void OnMusicVolumeChange(float value)
    {
        AudioController.Instance.SetMusicVolume(value);
        musicVolume.Value = value;
    }
}
