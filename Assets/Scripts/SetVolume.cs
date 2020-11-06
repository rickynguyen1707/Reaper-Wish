using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;

public class SetVolume : MonoBehaviour
{
    public AudioMixer musicMixer;
    public AudioMixer effectMixer;
    public void SetMusicLevel(float musicSliderValue)
    {
        musicMixer.SetFloat("MusicVol", Mathf.Log10(musicSliderValue) * 20);
    }
    public void SetSFXLevel(float effectSliderValue)
    {
        effectMixer.SetFloat("EffectVol", Mathf.Log10(effectSliderValue) * 20);
    }
}
