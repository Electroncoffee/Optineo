using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fork : MonoBehaviour
{

    public PlayerMove player;
    float disspawn_time = 2.4f;
    float stay_time = 0;

    void Awkae()
    {
        
    }
    

    void Update()
    {
        stay_time += Time.deltaTime;

        if(stay_time > disspawn_time)
        {
            killed();
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject == player.gameObject)
        {
            //플레이어 사망
            player.dead();
            Debug.Log("Death");
        }
    }

    void killed()
    {
        //보스의 특정 패턴에 의한 사망
        Destroy(gameObject);
    }
}
