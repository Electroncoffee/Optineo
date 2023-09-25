using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEvent : MonoBehaviour
{
    public ObjectLayerManager LayerManager;
    public GameObject parent;
    public float time = 0.5f;
    SpriteRenderer sr;
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    public void Destroy()
    {
        LayerManager.RemoveObject(parent);
        Destroy(parent);
    }

    private IEnumerator FadeOut()
    {
        Color startColor = sr.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0);
        float elapsedTime = 1f;

        while (elapsedTime < time)
        {
            sr.color = new Color(1,1,1,elapsedTime);
            elapsedTime -= Time.deltaTime*1.5f;
            yield return null;
        }

    }
}
