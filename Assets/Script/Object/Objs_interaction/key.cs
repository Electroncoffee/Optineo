using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class key : MonoBehaviour, icall
{
    public Inventory item;
    public HpManager hp;
    public ObjectLayerManager LayerManager;
    public SceneSoundManager soundManager;
    private AudioSource audioSource;
    public GameObject key_effect;
    bool flag = true;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }
    public void call(Vector3 pos)
    {
        if (flag)
        {
            flag = false;
            hp.damage(1);
            audioSource.Play();
            key_effect.SetActive(true);
            item.has_key = true;

        }
    }
}
