using System.Collections;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;


public class Break_Wine : MonoBehaviour, icall
{
    public int damage;
    public float moveSpeed;
    private bool isActive = false;
    private Vector2 velocity = Vector2.zero;
    private Vector3 End_pos;

    public PlayerMove playerScript;
    public HpManager hp;
    public ObjectLayerManager LayerManager;
    public SceneSoundManager soundManager;
    private Collider2D col;
    private Vector2 size;

    //private AudioSource audioSource; //오브젝트 자체에서 재생하도록 할 때 활성화
   
    private void Awake()
    {
        size = new Vector2(63 / 64, 63 / 64);
    }

   void FixedUpdate()
   {
        if(isActive) //call 함수가 작동하여 플레이어가 넘어져야한다면
        {
            if(playerScript.isStop) // 플레이어가 필드 타일의 중앙에 도달한 뒤
            {
                if(col != null) //앞의 장애물이 있는데도 미끄러지는 상황. 머리를 박는 애니메이션이 들어가면 좋을듯
                {
                    hp.damage(damage); //체력 감소
                    isActive = false;
                    playerScript.flag_isActing(true, 1.0f);
                    return;                    
                }

                playerScript.transform.position = Vector2.SmoothDamp(playerScript.transform.position, End_pos, ref velocity, Time.deltaTime * moveSpeed); //강제 이동

              
                //플레이어블 캐릭터의 넘어지는 모션 추가
                //플레이어블 캐릭터의 넘어지는 사운드 추가

                
            }

            if(playerScript.transform.position == End_pos)
            {
                hp.damage(damage); //체력 감소
                isActive = false;
                playerScript.flag_isActing(true, 1.0f);
            }

           
        }
   }

    public void call(Vector3 pos)
    {
        isActive = true;
        End_pos = transform.position + pos;
        playerScript.flag_isActing(false);
        col = Physics2D.OverlapBox(transform.position + pos, size, 0, LayerMask.GetMask("Block", "Object"));
    }





}
