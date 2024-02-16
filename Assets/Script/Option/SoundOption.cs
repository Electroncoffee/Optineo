using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundOption : MonoBehaviour
{
    //BGM 및 SFX 볼륨 조절


    public Slider bgmSlider;
    public Slider sfxSlider;
    public SoundManager soundmanager;

    void Start()
    {
        bgmSlider.value = soundmanager.bgmVolume * 100;
        sfxSlider.value = soundmanager.sfxVolume * 100;
    }

    public void UpdatebgmVolume()
    {
        soundmanager._audioSources[(int)Sound.Bgm].volume = bgmSlider.value / 100;
    }
    public void UpdatesfxVolume()
    {
        soundmanager._audioSources[(int)Sound.Effect].volume = sfxSlider.value / 100;
    }

}
