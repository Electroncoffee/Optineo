using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBush : MonoBehaviour, icall
{
    public int damage;
    public PlayerMove playerScript;
    public HpManager hp;
    public ObjectLayerManager LayerManager;
    public SceneSoundManager soundManager;
    public AudioClip clip;
    public void call(Vector3 pos)
    {
        soundManager.Play(clip);
        hp.damage(damage);
        playerScript.flag_isActing(true,0.1f);
        LayerManager.RemoveObject(this.gameObject);
        Destroy(this.gameObject);
    }
}
