using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathTest : MonoBehaviour
{

    public float current_A;
    public float current_B;

    public float target_result;

    void Awake()
    {
        current_A = 0f;
        current_B = 0f;
        target_result = 10f;
    }

    void Update()
    {

        if(Input.GetKey(KeyCode.T))
        {


            current_A += 0.1f;

            current_A = Mathf.Clamp(current_A, 0f, 100f);
            current_A = Mathf.Lerp(current_A, 100f, 0.01f);
            


        }

        if(Input.GetKey(KeyCode.Y))
        {
            current_A -= 0.1f;

            current_A = Mathf.Clamp(current_A, 0f, 100f);
            current_A = Mathf.Lerp(current_A, 0f, 0.01f);
        }
        
    }
}
