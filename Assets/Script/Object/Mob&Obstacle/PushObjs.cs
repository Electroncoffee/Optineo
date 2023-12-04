using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

게임 속 플레이어와 상호작용에서 움직이는(좌표가 변하는) 물체들에 대한 기본적인 정보를 담는 스크립트
현재 적용될 대상 : 석상, 쥐, 와인병

*/
public class PushObjs : MonoBehaviour
{
    public float moveSpeed; // 이동 속도
    public float moveDistance; // 이동 거리
    private bool Move = false; // 정지 여부
    private Vector3 target_pos; //이동할 좌표
    private Vector2 size;
    private Collider2D col;
    private float shakeAmount = 0.2f; // 흔들림의 강도
    private float shakeDuration = 0.1f; // 흔들림 지속 시간
    private Vector3 initialPosition; //쉐이킹 좌표
    private AudioSource audioSource;
    public SceneSoundManager soundManager;
    public PlayerMove playerScript;
    public HpManager hpManager;

     private void Awake()
    {
        size = new Vector2(63 / 64, 63 / 64);
        audioSource = GetComponent<AudioSource>();
    }


}
