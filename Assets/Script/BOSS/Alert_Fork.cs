using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alert_Fork : MonoBehaviour
{
    public float spawn_time = 3f; //미니언 소환까지의 시간
    public GameObject Fork;
    
    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
        anim.Play("Alert");
    }

    void Update()
    {
        spawn_time -= Time.deltaTime;
        if(spawn_time < 0)
        {
            spawn_Fork();
        }
    }

    void spawn_Fork()
    {
        Instantiate(Fork, transform.position, Quaternion.Euler(0,0,180), transform.parent);
        Destroy(gameObject);
    }

    public void call(Vector3 pos)
    {
        return;
    }
}
