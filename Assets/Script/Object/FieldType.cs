using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldType : MonoBehaviour, icall
{
    public int damage;
    public AudioClip clip;
    public SoundManager soundManager;
    public HpManager hpManager;
    public void call(Vector3 pos)
    {
        soundManager.Play(clip);
        hpManager.damage(damage);
    }
    void Start()
    {
    }
}