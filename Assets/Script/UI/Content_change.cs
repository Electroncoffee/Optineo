using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Content_change : MonoBehaviour
{
    public Text text;


    private ScrollRect sr;

    //public Button btn;


    private void Start()
    {
        sr = GetComponent<ScrollRect>();
    }
    public void change(RectTransform content)
    {
        sr.content.gameObject.SetActive(false);
        sr.content = content;
        content.gameObject.SetActive(true);
        text.text = sr.content.ToString();
    }
}
