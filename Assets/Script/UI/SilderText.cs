using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SilderText : MonoBehaviour
{

    public Slider slider;
    public TextMeshProUGUI text;


    public void substituteText()
    {
        float temp = slider.value;
        text.text = slider.value.ToString();
        text.text += "%";
    }
}
