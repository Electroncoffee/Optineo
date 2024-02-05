using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionControl : MonoBehaviour
{
    //구현 예정 목록
    //1. BGM 및 SFX 볼륨 조절
    //2. 텍스트 속도
    //3. 키보드 조작
    //4. 언어설정
    //5. 해상도 조절
    //6. 전체화면, 창모드 조절

    public Slider bgmSlider;
    public Slider sfxSlider;
    public SoundManager soundmanager;

    void Start()
    {
        bgmSlider.value = soundmanager.bgmVolume;
        sfxSlider.value = soundmanager.sfxVolume;
    }

    public void UpdatebgmVolume()
    {
        soundmanager._audioSources[(int)Sound.Bgm].volume = bgmSlider.value;
    }
    public void UpdatesfxVolume()
    {
        soundmanager._audioSources[(int)Sound.Effect].volume = sfxSlider.value;
    }

}
