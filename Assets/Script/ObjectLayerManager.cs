using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**************************
 * 오브젝트의 Y값을 기준으로 스프라이트 레이어값을 변경하는 스크립트
 *************************/
public class ObjectLayerManager : MonoBehaviour
{
    GameObject[] objects; // "Object" 태그를 가진 오브젝트를 찾습니다.
    private void Awake()
    {
        objects = GameObject.FindGameObjectsWithTag("LayerObject");//항상 LoadXml뒤에 나와야한다.
    }
    private void Update()
    {
        Layer_Update();
    }
    private void Layer_Update()
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
    private void OnRenderObject()// 동적으로 오브젝트가 나올경우를 대비
    {
        objects = GameObject.FindGameObjectsWithTag("LayerObject");
    }
}
