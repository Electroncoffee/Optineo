using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text Hp;
    public void Hp_Update(int num)
    {
        Hp.text = num.ToString();
    }
}
