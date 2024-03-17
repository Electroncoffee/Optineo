using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fork : MonoBehaviour
{

    public PlayerMove player;
    public ObjectLayerManager LayerManager;
    public BoxCollider2D colider2d;
    Animator anim;
    float disspawn_time = 1f;
    float stay_time = 0;

    void Awake()
    {
        colider2d = GetComponent<BoxCollider2D>();
        player = FindObjectOfType<PlayerMove>().GetComponent<PlayerMove>();
        LayerManager = FindObjectOfType<ObjectLayerManager>().GetComponent<ObjectLayerManager>();
        anim = transform.GetComponent<Animator>();
        anim.Play("Fork_hitGround");
        
    }
    

    void Update()
    {
        if(colider2d.enabled)
        {
            if(transform.position == player.transform.position)
            {
                Attack();
            }
        }

        stay_time += Time.deltaTime;

        if(stay_time > disspawn_time)
        {
            killed();
        }
    }

    void Attack()
    {
        player.dead();
        Debug.Log("Death");
        killed();
        Time.timeScale = 0;
    }



    void killed()
    {
        //일정 시간 이후 제거
        LayerManager.RemoveObject(gameObject);

        Destroy(gameObject);
    }
}
