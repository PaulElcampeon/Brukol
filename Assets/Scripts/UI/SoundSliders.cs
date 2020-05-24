using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SoundSliders : MonoBehaviour
{
    [SerializeField]
    private Slider bgmSlider;

    [SerializeField]
    private Slider sfxSlider;

    private void OnEnable()
    {
        bgmSlider.value = SoundManager.instance.bgmVolume;

        sfxSlider.value = SoundManager.instance.sfxVolume;
    }

    public void UpdateBGMVolume()
    {
        SoundManager.instance.UpdatetBGMVolume(bgmSlider.value);
    }

    public void UpdateSFXVolume()
    {
        SoundManager.instance.UpdatetSFXVolume(sfxSlider.value);

    }
}
