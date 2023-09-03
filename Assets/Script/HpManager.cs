using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class HpManager : MonoBehaviour
{
    public int MaxHp = 10;
    public int Hp;
    public UIManager uiManager;
    private void Start()
    {
        Hp = MaxHp;
        uiManager.Hp_Update(Hp);
    }
    public void damage(int d)
    {
        Hp -= d;
        uiManager.Hp_Update(Hp);
    }
}