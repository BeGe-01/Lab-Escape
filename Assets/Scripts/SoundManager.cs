using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    private AudioSource sfxSource;
    private AudioSource musicSource;
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        instance = this;
        sfxSource = GetComponent<AudioSource>();
        musicSource = transform.GetChild(0).GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip _sound)
    {
        sfxSource.PlayOneShot(_sound);
    }
}
