using UnityEngine;
using UnityEngine.Timeline;

public class Alert_minion : MonoBehaviour, icall
{
    
    public float spawn_time = 3f; //미니언 소환까지의 시간
    public GameObject minion;
    
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
            spawn_minion();
        }
    }

    void spawn_minion()
    {
        Instantiate(minion, transform.position, Quaternion.Euler(0,0,0), transform.parent);
        Destroy(gameObject);
    }

    public void call(Vector3 pos)
    {
        return;
    }

}
