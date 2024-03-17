using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Boss : MonoBehaviour
{
    //공용 변수
    [Header("Public Variable")]
    public GameObject boss;
    Collider2D col;
    Vector2 size = new Vector2 (63/64, 63/64);

    //플레이어 관련 변수
    public PlayerMove playerScript;
    public float death_time = 5f;
    float death_timer = 0;
    public int count = 0;
    
    //미니언 관련 변수
    [Header("Minion")]
    public GameObject Alert_minion;
    public float minion_coltime = 3f;
    float minion_timer;

    //포크, 나이프 관련 변수
    [Header("Fork")]
    public GameObject Alert_Fork;
    public float Fork_coltime = 3f;
    float Fork_timer;
    public int Fork_Number = 4; //한번에 소환될 포크의 개수

    [Header("Pattern Cooltime")]
    public bool isPattern = false; //패턴이 진행 중일 때 다른 패턴이 겹치지 않도록 하기 위함

    [Header("Debug")]
    public float pattern_col = 3f;
    float pattern_timer = 0;
    public bool minion_spawn = true;
    public bool fork_spawn = true;
    public bool oil_spawn = true;
    public bool thorns_spawn = true;
    
    void Awake()
    {
        Fork_Number -= 1; //플레이어의 위치에 꽂히는 포크는 Fork_number에서 제외되므로 -1
        Fork_Number = Mathf.Clamp(Fork_Number, 1, 10); //소환될 포크의 개수 최대값을 제한
    }

    void Update()
    {
        if(!isPattern && pattern_timer < pattern_col) pattern_timer += Time.deltaTime;
        if(minion_timer < minion_coltime) minion_timer += Time.deltaTime;
        if(Fork_timer < Fork_coltime) Fork_timer += Time.deltaTime;
        instance_death();


        if(!isPattern && pattern_timer > pattern_col)
        {
            if(count > 3)
            {
                //Tongue_injection();
                count = 0;
                return;
            }


            int ran = Random.Range(0,4);
            switch(ran)
            {
                case 0:
                    if(minion_spawn) spawn_Minion();
                    break;
                case 1:
                    if(fork_spawn) spawn_Fork();
                    break;
                case 2:
                    if(oil_spawn) Oil_overflow();
                    break;
                case 3:
                    if(thorns_spawn) Thorns_Rulker();
                    break;

            }
            pattern_timer = 0;
        }

    }

    void instance_death()
    {
        //일정 시간 이상 플레이어의 isStop 상태가 변하지 않을 경우 플레이어 즉사
        if(playerScript.isStop)
        {
            death_timer += Time.deltaTime;
            if(death_time < death_timer)
            {
                Debug.Log("Player die");
                death_timer = 0;
            }
        }
        else
        {
            death_timer = 0;
        }
    }
    
    void spawn_Minion()
    {

        if(minion_timer < minion_coltime)
        {
            return;
        }


        isPattern = true;
        Vector2 target_pos = playerScript.transform.position;
        int ranint = 0;
        for(;col == null;)
        {
            ranint = Random.Range(1,4);
            switch(ranint)
            {
                case 1:
                target_pos = target_pos + Vector2.up;
                break;
                case 2:
                target_pos = target_pos + Vector2.left;
                break;
                case 3:
                target_pos = target_pos + Vector2.right;
                break;
                case 4:
                target_pos = target_pos + Vector2.down;
                break;
            }

            target_pos.x = Mathf.Round(target_pos.x);
            target_pos.y = Mathf.Round(target_pos.y);

            col = Physics2D.OverlapBox(target_pos, size, 0, LayerMask.GetMask("Block", "Object", "Field"));

            if(target_pos.x <-3 || target_pos.x > 3 || target_pos.y < 0 || target_pos.y > 4)
            {
                //만약 맵 바깥까지 좌표가 나갈 경우
                minion_timer = 0;
                col = null;
                StartCoroutine(isPatternSwitch(false, 8f));
                break;
            }

            if(col == null)
            {
                //오브젝트 소환
                Instantiate(Alert_minion, target_pos, Quaternion.Euler(0,0,0), transform);
                minion_timer = 0;
                StartCoroutine(isPatternSwitch(false, 8f));
                count++;
                return;
            }
            else
            {
                //해당 반복문을 다시 시작
                col = null;
            }


        }


    
    }

    void spawn_Fork()
    {

        if(Fork_timer < Fork_coltime)
        {
            return;
        }

        isPattern = true;

        //랜덤 좌표 설정
        bool check;
        Vector2[] fork_pos = new Vector2[Fork_Number];
        int fork_pos_x;
        int fork_pos_y;
        int n = 0;

        for(;n < Fork_Number;)
        {
            check = true;
            fork_pos_x = Random.Range(-3,3);
            fork_pos_y = Random.Range(0,4);
            Vector2 check_pos = new Vector2(fork_pos_x, fork_pos_y);
            Vector2 player_p = playerScript.transform.position;
            player_p.x = Mathf.Round(player_p.x);
            player_p.y = Mathf.Round(player_p.y);

            col = Physics2D.OverlapBox(check_pos, size, 0, LayerMask.GetMask("Block", "Object", "Field"));
            if(col == null)
            {
                for(int i = 0; i < Fork_Number;i++)
                {
                    if(check_pos == fork_pos[i] || check_pos == player_p)
                    {
                        check = false;
                    }
                }

                if(check) fork_pos[n++] = check_pos;

            }

        }


        Vector2 target_pos = playerScript.transform.position;
        for(;col == null;)
        {
            target_pos.x = Mathf.Round(target_pos.x);
            target_pos.y = Mathf.Round(target_pos.y);


            col = Physics2D.OverlapBox(target_pos, size, 0, LayerMask.GetMask("Block", "Object", "Field"));


            if(target_pos.x <-3 || target_pos.x > 3 || target_pos.y < 0 || target_pos.y > 4)
            {
                //만약 맵 바깥까지 좌표가 나갈 경우
                Fork_timer = 0;
                col = null;
                StartCoroutine(isPatternSwitch(false, 5f));
                break;
            }

            if(col == null)
            {
                //오브젝트 소환
                Instantiate(Alert_Fork, target_pos, Quaternion.Euler(0,0,0), transform);
                for(int i = 0; i < Fork_Number;i++)
                {
                    Instantiate(Alert_Fork, fork_pos[i], Quaternion.Euler(0,0,0), transform);
                }
                Fork_timer = 0;
                StartCoroutine(isPatternSwitch(false, 5f));
                count++;
                return;
            }
            else
            {
                //해당 반복문을 다시 시작
                col = null;
            }


        }

    }

    void Rage_Berserk()
    {
        //페이즈의 개념으로 보면 편함
        //총 3페이즈가 있을 예정
    }
    
    void Rage_Howl()
    {

    }

    void Oil_overflow()
    {
        //세로 2줄 정도의 범위를 가질 예정
        //+가능하면 미니언이 밀려나면 더 좋을듯
    }

    void Thorns_Rulker()
    {

    }

    void Tongue_injection()
    {

    }
    

    private IEnumerator isPatternSwitch(bool check, float wait)
    {
        yield return new WaitForSeconds(wait);
        isPattern = check;
    }
}
