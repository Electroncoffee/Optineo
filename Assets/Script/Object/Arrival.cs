using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrival : MonoBehaviour, icall
{
    public Inventory inven;
    AudioClip clip;
    public void call(Vector3 pos)
    {
        if (inven.has_key)
        {
            Debug.Log("도착");
        }
    }
}
