using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingManager : MonoBehaviour
{
    Inventory key;
    private void Awake()
    {
        Object.DontDestroyOnLoad(this);
        key.has_key = false;
    }
    void Update()
    {
        
    }
}
