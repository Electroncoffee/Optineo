using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Clock : MonoBehaviour
{
    public bool showcurrenttime;
    public TextMeshProUGUI timer;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(currenttime());
    }
    IEnumerator currenttime()
    {
        while (showcurrenttime == true)
        {
            timer.text = System.DateTime.Now.ToShortTimeString();
            yield return null;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
