using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ORat : MonoBehaviour, icall
{
    public float moveSpeed; // 이동 속도
    public float moveDistance; // 이동 거리
    private bool Move = false; // 정지 여부
    private Vector3 target_pos; //이동할 좌표
    private Vector2 size;
    private Collider2D col; //석상의 이동방향에 존재하는 오브젝트의 정보를 받아오기 위한 콜라이더
    private AudioSource audioSource; //쥐의 경우 오디오 소스를 쥐 밀림, 쥐 사망 으로 적어도 2개의 오디오 소스가 필요함
    public ObjectLayerManager LayerManager;
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

        if(this.gameObject != null)
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


        
    }

    public void call(Vector3 pos)
    {
        col = Physics2D.OverlapBox(transform.position + pos, size, 0, LayerMask.GetMask("Block", "Object"));
        audioSource.Play();
        hpManager.damage(1);
        if (col == null) // 이동방향에 아무것도 없음
        {
            Debug.Log("Error");
            target_pos = transform.position + (pos * moveDistance);

            transform.position = target_pos;

            playerScript.flag_isActing(true,1.0f);

            Move = true;
            Debug.Log("Error");
        }
        else
        {
            StartCoroutine(Dead_Obj());
        }
        
    }

    private IEnumerator Dead_Obj()
    {
        
        hpManager.damage(1);

        //쥐가 죽는 애니메이션 추가 필요
        LayerManager.RemoveObject(this.gameObject);
        Destroy(this.gameObject);

        yield return null;
    }
}
