using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FBreak_Wine : MonoBehaviour, icall
{
    public int damage;
    public int moveSpeed;
    public int moveDistance;
    public PlayerMove playerScript;
    public HpManager hp;
    public ObjectLayerManager LayerManager;
    public SceneSoundManager soundManager;

    //private AudioSource audioSource; //오브젝트 자체에서 재생하도록 할 때 활성화


    public void call(Vector3 pos)
    {

        //플레이어의 넘어지는 애니메이션을 해당 함수에서 재생할 지, PlayerMove 스크립트에서 재생할 지 선택
        //밟고 미끄러지는 효과음 재생 필요

        hp.damage(damage);

        playerScript.transform.position = playerScript.transform.position + (pos * moveDistance);
        //플레이어가 미끄러질 때 대기시간이나 속력 등을 조절할 수 있게 바꿀 필요가 있음

        playerScript.flag_isActing(true,0.1f);

    }
}
