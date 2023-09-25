using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class key : MonoBehaviour, icall
{
    public Inventory item;
    public ObjectLayerManager LayerManager;
    public SceneSoundManager soundManager;
    public AudioClip clip;
    public GameObject key_effect;
    SpriteRenderer sr;
    bool flag = true;
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    public void call(Vector3 pos)
    {
        if (flag)
        {
            flag = false;
            soundManager.Play(clip);
            key_effect.SetActive(true);
            item.has_key = true;
        }
    }
}
