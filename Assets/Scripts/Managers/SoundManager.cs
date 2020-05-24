using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("Sound Effect Sounds")]
    [SerializeField]
    private AudioSource[] sfxSounds;

    [Header("Back Ground Music Sounds")]
    [SerializeField]
    private AudioSource[] bgmSounds;

    public float sfxVolume { get; set; }
    public float bgmVolume { get; set; }

    public static SoundManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        sfxVolume = 0.3f;
        bgmVolume = 0.2f;

        PlayBGM(0);
    }

    public void PlaySFX(int sfxIndex)
    {
        AudioSource sound = sfxSounds[sfxIndex];

        sound.volume = sfxVolume;
        sound.Play();
    }

    public void PlayBGM(int bgmIndex)
    {
        AudioSource sound = bgmSounds[bgmIndex];

        sound.volume = bgmVolume;
        sound.Play();
    }

    public void StopCurrentBGM()
    {
        for (int i = 0; i < bgmSounds.Length; i++)
        {
            if (bgmSounds[i].isPlaying) bgmSounds[i].Stop();
        }
    }

    private void UpdateBGMThatMayBePlaying()
    {
        for (int i = 0; i < bgmSounds.Length; i++)
        {
            if (bgmSounds[i].isPlaying) bgmSounds[i].volume = bgmVolume;
        }
    }

    public void UpdatetBGMVolume(float volume)
    {
        bgmVolume = volume;

        UpdateBGMThatMayBePlaying();

        Debug.Log("BGM Volume is now: " + volume);
    }

    public void UpdatetSFXVolume(float volume)
    {
        sfxVolume = volume;

        Debug.Log("SFX Volume is now: " + volume);
    }
}
