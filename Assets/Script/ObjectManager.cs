using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    GameObject[] objects; // "Object" 태그를 가진 오브젝트를 찾습니다.
    void Start()
    {
        objects = GameObject.FindGameObjectsWithTag("LayerObject");
    }
    void Update()
    {
        foreach (GameObject obj in objects)
        {
            SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();

            if (spriteRenderer != null)
            {
                int sortingOrder = Mathf.RoundToInt(-obj.transform.position.y);
                spriteRenderer.sortingOrder = sortingOrder;
            }
        }
    }
}
