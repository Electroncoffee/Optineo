using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phone : MonoBehaviour
{
    public void ObjectActive()
    {
        this.gameObject.SetActive(!this.gameObject.activeSelf);
    }
}
