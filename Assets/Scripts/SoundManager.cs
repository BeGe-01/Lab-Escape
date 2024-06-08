using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    private AudioSource sfxSource;
    private AudioSource musicSource;
    void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(instance);

        }
        instance = this;
        sfxSource = GetComponent<AudioSource>();
        musicSource = transform.GetChild(0).GetComponent<AudioSource>();
        SetSoundVolume(SaveManager.instance.saveData.sound);
        SetMusicVolume(SaveManager.instance.saveData.music);
    }

    public void PlaySound(AudioClip _sound)
    {
        sfxSource.PlayOneShot(_sound);
    }

    public void SetSoundVolume(float value)
    {
        sfxSource.volume = value;
    }
    public void SetMusicVolume(float value)
    {
        musicSource.volume = value;
    }
}
