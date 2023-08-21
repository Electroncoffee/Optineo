using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Vector3 Player_Start_Pos; //이동시 출발 좌표
    private Vector3 target_pos; //이동할 좌표
    private Vector3 delta_pos; //수정치 좌표
    Dictionary<KeyCode, Vector3> Move_Key;
    public float moveSpeed; // 이동 속도
    public float moveDistance; // 이동 거리
    public bool isStop = true; // 정지 여부
    void Start()
    {
        Move_Key = new Dictionary<KeyCode, Vector3>();
        Move_Key[KeyCode.UpArrow] = new Vector3(0, moveDistance, 0); ; // 상
        Move_Key[KeyCode.DownArrow] = new Vector3(0, -moveDistance, 0); ; // 하
        Move_Key[KeyCode.LeftArrow] = new Vector3(-moveDistance, 0, 0); ; // 좌
        Move_Key[KeyCode.RightArrow] = new Vector3(moveDistance, 0, 0); ; // 우
    }
    void Update()
    {
        if (isStop)
        {
            foreach (KeyValuePair<KeyCode, Vector3> item in Move_Key)
            {
                if (Input.GetKeyDown(item.Key))
                {
                    isStop = false;
                    Player_Start_Pos = transform.position;
                    target_pos = Player_Start_Pos + (item.Value * moveDistance);
                }
            }
        }
    }
    private void FixedUpdate()
    {
        if (!isStop)
            transform.position = Vector3.MoveTowards(transform.position, target_pos, Time.deltaTime * moveSpeed);
        if (transform.position.Equals(target_pos))
            isStop = true;
    }
}

