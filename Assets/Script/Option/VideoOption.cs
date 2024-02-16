using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VideoOption : MonoBehaviour
{
    FullScreenMode screenMode;
    public TMP_Dropdown resolutionDropdown;
    public Toggle fullscreenBtn;
    List<Resolution>resolutions = new List<Resolution>();
    public int resolutionNum;

    void Start()
    {
        InitUI();
    }

    void InitUI()
    {
        // for (int i = 0; i < Screen.resolutions.Length; i++)
        // {
        //     if(Screen.resolutions[i].refreshRate == 60)
        //         resolutions.Add(Screen.resolutions[i]);
        // } 해당 코드는 분류를 위한 코드 (ex 프레임 60 해상도만 표시하기 등)
        resolutions.AddRange(Screen.resolutions);
        resolutionDropdown.options.Clear();

        int optionNum = 0;
        foreach(Resolution item in resolutions)
        {
            TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData();
            option.text = item.width + " x" + item.height + " " + item.refreshRate + " hz";
            resolutionDropdown.options.Add(option);

            if(item.width == Screen.width && item.height == Screen.height)
                resolutionDropdown.value = optionNum;
            optionNum++;
             
        }
        resolutionDropdown.RefreshShownValue();

        fullscreenBtn.isOn = Screen.fullScreenMode.Equals(FullScreenMode.FullScreenWindow) ? true:false;
    }

    public void DropboxOptionChange(int x)
    {
        resolutionNum = x;
    }

    public void FullScrrenBtn(bool isFull)
    {
        screenMode = isFull ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
    }

    public void OKbtnClick()
    {
        Screen.SetResolution(resolutions[resolutionNum].width, resolutions[resolutionNum].height, screenMode);
    }

}
