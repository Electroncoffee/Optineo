using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alert_Thorn : MonoBehaviour, icall
{
    
    public GameObject thorn;
    public SpriteRenderer sprite;
    public float spawn_time;
    public float invisble_time = 2f;
    public Animator anim;

    void Awake()
    {
        switch(transform.position.y)
        {
            case 4:
                spawn_time = 0.5f;
                break;
            case 3:
                spawn_time = 1f;
                break;
            case 2:
                spawn_time = 1.5f;
                break;
            case 1:
                spawn_time = 2f;
                break;
            case 0:
                spawn_time = 2.5f;
                break;
        }
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        anim.Play("Alert");
    }

    void Update()
    {
        invisble_time -= Time.deltaTime;

        if(invisble_time < 0)
        {
            sprite.enabled = false;

            spawn_time -= Time.deltaTime;

            if(spawn_time < 0)
            {
                Instantiate(thorn, transform.position, Quaternion.Euler(0,0,0), transform.parent);

                Destroy(gameObject);
            }
        }
    }

    public void call(Vector3 pos)
    {
        return;
    }
    
}
