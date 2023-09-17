using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class HpManager : MonoBehaviour
{
    public int Hp;
    public UIManager uiManager;
    public UnityEvent dead;
    SettingManager settingManager;
    private void Awake()
    {
        settingManager = FindObjectOfType<SettingManager>();
    }
    public void HPSetting(int MaxHp)
    {
        Hp = MaxHp;
        uiManager.Hp_Update(Hp);
    }
    public void damage(int d)
    {
        Hp -= d;
        if(Hp<=0)
        {
            Hp = 0;
            dead.Invoke();
            //Time.timeScale = 0;
        }
        uiManager.Hp_Update(Hp);
    }
}