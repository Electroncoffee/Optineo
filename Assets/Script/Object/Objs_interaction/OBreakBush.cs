using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OBreakBush : MonoBehaviour, icall
{
    public int damage;
    public PlayerMove playerScript;
    public HpManager hp;
    public ObjectLayerManager LayerManager;
    public SceneSoundManager soundManager;
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void call(Vector3 pos)
    {
        audioSource.Play();
        hp.damage(damage);
        playerScript.flag_isActing(true,0.1f);
        LayerManager.RemoveObject(this.gameObject);
        
        Destroy(this.gameObject);
        
    }




}
