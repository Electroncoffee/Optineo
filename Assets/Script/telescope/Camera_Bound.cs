using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Bound : MonoBehaviour
{
    public Camera camera_ = null;

    private float size_y_;
    private float size_x_;

    public float Bottom
    {
        get
        {
            return size_y_ * -1 + camera_.gameObject.transform.position.y;
        }
    }

    public float Top
    {
        get
        {
            return size_y_ + camera_.gameObject.transform.position.y;
        }
    }

    public float Left
    {
        get
        {
            return size_x_ * -1 + camera_.gameObject.transform.position.x;
        }
    }

    public float Right
    {
        get
        {
            return size_x_ + camera_.gameObject.transform.position.x;
        }
    }

    public float Height
    {
        get
        {
            return size_y_ * 2;
        }
    }

    public float Width
    {
        get
        {
            return size_x_ * 2;
        }
    }

    void Start()
    {
        size_y_ = camera_.orthographicSize;
        size_x_ = camera_.orthographicSize * Screen.width / Screen.height;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            string msg =
                string.Format("스크린 좌표: ({0}, {1}) ~ ({2}, {3}) : {4} x {5}",
                    Left, Top, Right, Bottom, Width, Height
                );

            Debug.Log(msg);
        }
    }
}