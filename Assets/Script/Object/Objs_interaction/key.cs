using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class key : MonoBehaviour, icall
{
    public Inventory item;
    public ObjectLayerManager LayerManager;
    public SceneSoundManager soundManager;
    private AudioSource audioSource;
    public GameObject key_effect;
    SpriteRenderer sr;
    bool flag = true;
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }
    public void call(Vector3 pos)
    {
        if (flag)
        {
            flag = false;
            audioSource.Play();
            key_effect.SetActive(true);
            item.has_key = true;
            LayerManager.RemoveObject(this.gameObject);
            Destroy(this.gameObject);
        }
    }
}
