using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed; // 이동 속도
    public float moveDistance; // 이동 거리
    public bool isStop = true; // 정지 여부
    public bool isActing = true;
    private Dictionary<KeyCode, Vector3> Move_Key;
    private AudioSource audioSource;
    private SpriteRenderer spriteRenderer;
    private Vector3 Player_Start_Pos; //이동시 출발 좌표
    private Vector3 target_pos; //이동할 좌표
    private Vector2 size;
    private Collider2D col;
    public HpManager hpManager;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Move_Key = new Dictionary<KeyCode, Vector3>();
        Move_Key[KeyCode.UpArrow] = new Vector3(0, moveDistance, 0); ; // 상
        Move_Key[KeyCode.DownArrow] = new Vector3(0, -moveDistance, 0); ; // 하
        Move_Key[KeyCode.LeftArrow] = new Vector3(-moveDistance, 0, 0); ; // 좌
        Move_Key[KeyCode.RightArrow] = new Vector3(moveDistance, 0, 0); ; // 우
        size = new Vector2(63 / 64, 63 / 64);

    }
    void Update() //너무 길고 네스트도 커져서 조금 분할할 필요가 있어보임
    {
        if (isStop & isActing)
        {
            foreach (KeyValuePair<KeyCode, Vector3> item in Move_Key)//이동키가 눌렸는지 전부 확인
            {
                if (Input.GetKey(item.Key)) //눌리면
                {
                    col = Physics2D.OverlapBox(transform.position + item.Value, size, 0, LayerMask.GetMask("Block","Object"));
                    if (col == null) // 이동방향에 아무것도 없음
                    {
                        audioSource.Play();
                        isStop = false; //움직이게
                        Player_Start_Pos = transform.position; //좌표저장(시작지점)
                        target_pos = Player_Start_Pos + (item.Value * moveDistance); //좌표저장(끝지점)
                        
                    }
                    else // 무언가 있음
                    {
                        target_pos = transform.position;
                        if (col.gameObject.layer == LayerMask.NameToLayer("Block"))
                        {
                            return;
                        }
                        if (col.gameObject.layer == LayerMask.NameToLayer("Object")) //오브젝트
                        {
                            isActing = false;
                            col.gameObject.GetComponent<ObjectMovement>().push(item.Value);//푸쉬 호출
                            audioSource.Play();
                            //발차기 애니메이션 넣어야함
                        }
                        
                    }
                    hpManager.Scan_Damaged(Physics2D.OverlapBox(target_pos, size, 0)?.gameObject);
                    if (item.Key == KeyCode.LeftArrow)//수평이동 flip처리
                        spriteRenderer.flipX = true;
                    else if (item.Key == KeyCode.RightArrow)
                        spriteRenderer.flipX = false;
                    return;
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

    public void flag_isActing(bool flag)
    {
        isActing = flag;
    }
}

/*
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Progress;

/**************************
 * 기본적인 이동 구현
 * horizontal -1,1 좌,우
 * vertical   -1,1 하,상
 * 오브젝트 룰 추가 후 이동조건 체크 추가해야함
 * 애니메이션 추가 해야함
 **************************

public class PlayerMove : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    int hori; int verti; //좌우, 상하
    int prehori; int preverti; //이전프레임 좌우, 상하
    private Vector3 Player_Start_Pos; //이동시 출발 좌표
    private Vector3 target_pos; //이동할 좌표
    private Vector3 delta_pos; //수정치 좌표
    public float moveSpeed; // 이동 속도
    public float moveDistance; // 이동 거리
    public bool isStop = true; // 정지 여부
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        hori = 0; verti = 0; prehori = 0; preverti = 0;
    }
    void Update()
    {
        hori = (int)Input.GetAxisRaw("Horizontal");
        verti = (int)Input.GetAxisRaw("Vertical");
        if (isStop)
        {
            if (hori != 0 && hori != prehori) //좌우
            {
                isStop = false; //움직이게
                Player_Start_Pos = transform.position; //좌표저장(시작지점)
                target_pos = Player_Start_Pos + (Vector3.right * hori * moveDistance); //좌표저장(끝지점)
                Debug.Log(target_pos);
                if (hori == 1)
                    spriteRenderer.flipX = false;
                else
                    spriteRenderer.flipX = true;
            }
            if (verti != 0 && verti != preverti) //상하
            {
                isStop = false; //움직이게
                Player_Start_Pos = transform.position; //좌표저장(시작지점)
                target_pos = Player_Start_Pos + (Vector3.up * verti * moveDistance); //좌표저장(끝지점)
            }
        }
        prehori = hori;
        preverti = verti;
    }
    private void FixedUpdate()
    {
        if (!isStop) //움직이면
            transform.position = Vector3.MoveTowards(transform.position, target_pos, Time.deltaTime * moveSpeed);
        if (transform.position.Equals(target_pos))
            isStop = true;
    }
}
*/