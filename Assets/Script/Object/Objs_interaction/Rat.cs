using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat : MonoBehaviour, icall
{
    public float moveSpeed; // 이동 속도
    public float moveDistance; // 이동 거리
    private bool Move = false; // 정지 여부
    private Vector3 target_pos; //이동할 좌표
    private Vector2 velocity = Vector3.zero; //smoothdamp 에서 사용할 변수
    private Vector2 size;
    private Collider2D col; //석상의 이동방향에 존재하는 오브젝트의 정보를 받아오기 위한 콜라이더
    public AudioClip clip;
    public ObjectLayerManager LayerManager;
    public SceneSoundManager soundManager;
    public PlayerMove playerScript;
    public HpManager hpManager;

    private void Awake()
    {
        size = new Vector2(63 / 64, 63 / 64);
    }


    private void FixedUpdate()
    {

        if(gameObject != null)
        {
            if (Move) //움직이면
            {
                transform.position = Vector2.SmoothDamp(transform.position, target_pos,ref velocity, Time.deltaTime * moveSpeed);
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
        soundManager.Play(clip);
        hpManager.damage(1);
        if (col == null) // 이동방향에 아무것도 없음
        {
            target_pos = transform.position + (pos * moveDistance);

            playerScript.flag_isActing(true,1.0f);

            Move = true;
        }
        else
        {

            LayerManager.RemoveObject(gameObject);

            soundManager.Play(clip);

            Destroy(gameObject, clip.length);

            playerScript.flag_isActing(true, clip.length);
            
        }
        
    }

}
