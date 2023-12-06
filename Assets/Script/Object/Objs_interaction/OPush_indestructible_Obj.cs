using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OPush_indestructible_Obj : MonoBehaviour, icall
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
    private void FixedUpdate()
    {
        if (Move) //움직이면
        {
            transform.position = Vector3.MoveTowards(transform.position, target_pos, Time.deltaTime * moveSpeed);
            if (transform.position.Equals(target_pos))
            {
                playerScript.flag_isActing(true);
                Move = false;
            }
        }


    }


    public void call(Vector3 pos)
    {
        col = Physics2D.OverlapBox(transform.position + pos, size, 0, LayerMask.GetMask("Block", "Object"));
        audioSource.Play();
        hpManager.damage(1);
        if (col == null) // 이동방향에 아무것도 없음
        {
            target_pos = transform.position + (pos * moveDistance);

            transform.position = target_pos;

            playerScript.flag_isActing(true,1.0f);

            Move = true;
        }
        else
        {
            StartCoroutine(Shaking());
        }
    }
    private IEnumerator Shaking()
    {
        float elapsedTime = 0f;
        initialPosition = transform.position;
        while (elapsedTime < shakeDuration)
        {
            Vector3 randomOffset = UnityEngine.Random.insideUnitSphere * shakeAmount;
            transform.position = initialPosition + randomOffset;
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }
        transform.position = initialPosition;
        playerScript.flag_isActing(true);
    }
}
