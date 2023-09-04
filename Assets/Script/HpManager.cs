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
#nullable enable
    public void Scan_Damaged(GameObject? obj)
    {
        if (obj != null)
        {
            Debug.Log(obj.name);
        }
        if (obj == null || obj.layer != LayerMask.NameToLayer("Field"))
            damage(1);
        else
        {
            damage(obj.GetComponent<FieldType>().damage);
            obj.GetComponent<AudioSource>().Play();
        }
    }
#nullable disable
}