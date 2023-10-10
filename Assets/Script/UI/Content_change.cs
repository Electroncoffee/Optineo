using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Content_change : MonoBehaviour
{
    private ScrollRect sr;
    private void Start()
    {
        sr = GetComponent<ScrollRect>();
    }
    public void change(RectTransform content)
    {
        sr.content.gameObject.SetActive(false);
        sr.content = content;
        content.gameObject.SetActive(true);
    }
}
