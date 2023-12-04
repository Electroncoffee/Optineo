using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

게임 속 플레이어와 상호작용에서도 정지한 상태를 유지하는 물체들에 대한 기본적인 정보를 담는 스크립트
현재 적용될 대상 : 덤불, 깨진 와인, 썩은 식료품

*/

public class StayObjs : MonoBehaviour
{
    private AudioSource audioSource;
    public SceneSoundManager soundManager;
    public PlayerMove playerScript;
    public HpManager hpManager;

}
