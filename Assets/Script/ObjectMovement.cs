using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using static UnityEditor.Progress;

public class ObjectMovement : MonoBehaviour
{
    public float moveSpeed; // 이동 속도
    public float moveDistance; // 이동 거리
    private bool isStop = true; // 정지 여부
    private Vector3 Player_Start_Pos; //이동시 출발 좌표
    private Vector3 target_pos; //이동할 좌표
    public PlayerMove playerScript;

    private void FixedUpdate()
    {
        if (!isStop) //움직이면
            transform.position = Vector3.MoveTowards(transform.position, target_pos, Time.deltaTime * moveSpeed);
        if (transform.position.Equals(target_pos))
        {
            playerScript.flag_isActing(true);
        }
    }
    public void push(Vector3 pos)
    {
        playerScript.flag_isActing(false);
        target_pos = transform.position + (pos * moveDistance);
        isStop = false;
    }
}
