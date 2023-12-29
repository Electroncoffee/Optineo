using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
public class Camera_Movement : MonoBehaviour
{
    public float moveSpeed; // 이동 속도
    public float moveDistance; // 이동 거리
    public bool isStop; // 정지 여부
    public bool verti_able, hori_able;
    private Dictionary<KeyCode, Vector3> Move_Key = new Dictionary<KeyCode, Vector3>()
    {
        [KeyCode.UpArrow] = new Vector3(0, 1, 0), // 상
        [KeyCode.DownArrow] = new Vector3(0, -1, 0), // 하
        [KeyCode.LeftArrow] = new Vector3(-1, 0, 0), // 좌
        [KeyCode.RightArrow] = new Vector3(1, 0, 0), // 우
    };
    private Vector3 Camera_Start_Pos; //이동시 출발 좌표
    private Vector3 target_pos; //이동할 좌표
    private Vector2 size;
    private Collider2D col;

    void Awake()
    {
        isStop = true; // 정지 여부
        size = new Vector2(63 / 64, 63 / 64);
    }
    void Update() //너무 길고 네스트도 커져서 조금 분할할 필요가 있어보임
    {
        if (isStop)
        {
            foreach (KeyValuePair<KeyCode, Vector3> item in Move_Key)//이동키가 눌렸는지 전부 확인
            {
                if (Input.GetKey(item.Key)) //눌리면
                {
                    col = Physics2D.OverlapBox(transform.position + item.Value, size, 0, LayerMask.GetMask("C_Block"));
                    if (col == null) // 이동방향에 아무것도 없음
                    {
                        isStop = false; //움직이게
                        Camera_Start_Pos = transform.position; //좌표저장(시작지점)
                        target_pos = Camera_Start_Pos + (item.Value * moveDistance); //좌표저장(끝지점)
                    }
                    else
                        target_pos = transform.position;
                }
            }
        }
    }
    private void FixedUpdate()
    {
        if (!isStop) //움직이면
            transform.position = Vector3.MoveTowards(transform.position, target_pos, Time.deltaTime * moveSpeed);
        if (transform.position.Equals(target_pos))
            isStop = true;
    }
}