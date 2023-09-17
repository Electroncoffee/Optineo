using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**************************
 * 오브젝트의 Y값을 기준으로 스프라이트 레이어값을 변경하는 스크립트
 *************************/
public class ObjectLayerManager : MonoBehaviour
{
    GameObject[] Allobjects; // 모든 오브젝트
    GameObject[] noneobjects; // none 오브젝트
    List<GameObject> applyobjects;
    private void Start()
    {
        Allobjects = GameObject.FindObjectsOfType<GameObject>();
        noneobjects = GameObject.FindGameObjectsWithTag("none");
        applyobjects = new List<GameObject>();
        foreach (GameObject obj in Allobjects)
        {
            if (!ContainsObject(noneobjects, obj))
            {
                applyobjects.Add(obj);
            }
        }
    }
    private void Update()
    {
        Layer_Update();
    }
    private void Layer_Update()
    {
        foreach (GameObject obj in applyobjects)
        {
            SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();

            if (spriteRenderer != null)
            {
                int sortingOrder = Mathf.RoundToInt(-obj.transform.position.y);
                spriteRenderer.sortingOrder = sortingOrder;
            }
        }
    }
    public void AddObject(GameObject obj)// 동적으로 오브젝트가 나올경우를 대비
    {
        applyobjects.Add(obj);
    }
    public void RemoveObject(GameObject obj)
    {
        applyobjects.Remove(obj);
    }
    private bool ContainsObject(GameObject[] objects, GameObject obj)
    {
        foreach (GameObject item in objects)
        {
            if (item == obj)
            {
                return true;
            }
        }
        return false;
    }
    public void reloadObjectLayer()
    {
        Allobjects = GameObject.FindObjectsOfType<GameObject>();
        noneobjects = GameObject.FindGameObjectsWithTag("none");
        applyobjects = new List<GameObject>();
        foreach (GameObject obj in Allobjects)
        {
            if (!ContainsObject(noneobjects, obj))
            {
                applyobjects.Add(obj);
            }
        }
    }
}
