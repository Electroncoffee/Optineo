using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class HpManager : MonoBehaviour
{
    public int MaxHp = 10;
    public int Hp;
    public UIManager uiManager;
    public UnityEvent dead;
    public UnityEvent reset;
    private void Start()
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
            reset.Invoke();
        }
        uiManager.Hp_Update(Hp);
    }
/* 플레이어 이동방향의 타일을 탐색후 데미지가 있으면 체력을 깎는함수
#nullable enable
    public void Scan_Damaged(GameObject? obj)
    {
        if (obj != null && obj.layer == LayerMask.NameToLayer("Field"))//필드임
        {
            damage(obj.GetComponent<icallField>().Field_Damage());
            obj.GetComponent<clip>().Play();
        }
        else
        {
            damage(1);
        }
    }
#nullable disable
*/
}