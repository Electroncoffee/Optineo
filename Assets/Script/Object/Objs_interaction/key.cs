using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class key : MonoBehaviour, icall
{
    public Inventory item;
    public HpManager hp;
    public ObjectLayerManager LayerManager;
    public SceneSoundManager soundManager;
    public AudioClip clip;
    public GameObject key_effect;
    bool flag = true;

    public void call(Vector3 pos)
    {
        if (flag)
        {
            flag = false;
            hp.damage(1);
            soundManager.Play(clip);
            key_effect.SetActive(true);
            item.has_key = true;

        }
    }
}
