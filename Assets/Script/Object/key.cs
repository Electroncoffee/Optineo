using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class key : MonoBehaviour, icall
{
    public Inventory item;
    public ObjectLayerManager LayerManager;
    public SceneSoundManager soundManager;
    public AudioClip clip;
    public void call(Vector3 pos)
    {
        soundManager.Play(clip);
        item.has_key = true;
        LayerManager.RemoveObject(this.gameObject);
        Destroy(this.gameObject);

    }
    void Start()
    {
        
    }
}
