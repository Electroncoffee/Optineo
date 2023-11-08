using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed; // 이동 속도
    public float moveDistance; // 이동 거리
    public bool isStop; // 정지 여부
    public bool isActing;
    private Dictionary<KeyCode, Vector3> Move_Key;
    private Animator anim;
    private AudioSource audioSource;
    private SpriteRenderer spriteRenderer;
    private Vector3 Player_Start_Pos; //이동시 출발 좌표
    private Vector3 target_pos; //이동할 좌표
    private Vector2 size;
    private Collider2D col;
    public HpManager hpManager;

    void Awake()
    {
        isStop = true; // 정지 여부
        isActing = true;
        anim = GetComponent<Animator>();
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
        if (isStop && isActing)
        {
            foreach (KeyValuePair<KeyCode, Vector3> item in Move_Key)//이동키가 눌렸는지 전부 확인
            {
                if (Input.GetKey(item.Key)) //눌리면
                {
                    col = Physics2D.OverlapBox(transform.position + item.Value, size, 0, LayerMask.GetMask("Block","Object","Field","Item"));
                    if (col == null) // 이동방향에 아무것도 없음
                    {
                        anim.Play("Dash_3");
                        audioSource.Play();
                        isStop = false; //움직이게
                        Player_Start_Pos = transform.position; //좌표저장(시작지점)
                        target_pos = Player_Start_Pos + (item.Value * moveDistance); //좌표저장(끝지점)
                        hpManager.damage(1);
                        flip_x(item.Key);
                    }
                    else // 무언가 있음
                    {
                        target_pos = transform.position;
                        switch (LayerMask.LayerToName(col.gameObject.layer))
                        {
                            case "Block":
                                return;
                            case "Object":
                                isActing = false;
                                col.GetComponent<icall>().call(item.Value);
                                audioSource.Play();
                                flip_x(item.Key);
                                return;
                            case "Field":
                            case "Item":
                                anim.Play("Dash_3");
                                audioSource.Play();
                                isStop = false; //움직이게
                                Player_Start_Pos = transform.position; //좌표저장(시작지점)
                                target_pos = Player_Start_Pos + (item.Value * moveDistance); //좌표저장(끝지점)
                                col.GetComponent<icall>().call(item.Value);
                                flip_x(item.Key);
                                return;
                        }
                    }
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

    public void flip_x(KeyCode Key)
    {
        if (Key == KeyCode.LeftArrow)//수평이동 flip처리
            spriteRenderer.flipX = false;
        else if (Key == KeyCode.RightArrow)
            spriteRenderer.flipX = true;
    }
    public void flag_isActing(bool flag)
    {
        isActing = flag;
        Debug.Log("isActing now : " + flag);
    }
    public void flag_isActing(bool flag, float Time)
    {
        Debug.Log("isActing now : " + flag);
        StartCoroutine(Act_Func(flag, Time));
    }
    private IEnumerator Act_Func(bool flag, float Time)
    {
        yield return new WaitForSeconds(Time);
        isActing = flag;
    }
    public void dead()
    {
        anim.Play("Dead_");
    }
    public void Idle()
    {
        anim.Play("Idle_");
    }
}