using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInOut : MonoBehaviour
{
    public static FadeInOut instance;

    CanvasRenderer canvasRenderer;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
        canvasRenderer = GetComponent<CanvasRenderer>();
        canvasRenderer.SetAlpha(0);
        
    }

    // Update is called once per frame
    

    public void Transition()
    {
        StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {
        for (float i = 0; i <= 1; i += Time.deltaTime / 1)
        {
            canvasRenderer.SetAlpha(i);
            yield return null;
        }

        yield return new WaitForSeconds(1f);


        for (float i = 1; i >= 0; i -= Time.deltaTime / 1)
        {
            canvasRenderer.SetAlpha(i);
            yield return null;
        }

        

    }
}
