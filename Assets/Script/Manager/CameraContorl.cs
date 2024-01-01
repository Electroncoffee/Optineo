using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraContorl : MonoBehaviour
{

    public PlayerMove playerScript;
    private Camera camera;
    private Vector3 BasePos; //원래 상태로 복귀하기 위한 벡터
    Transform cameraTransform;


    public float Max_Size;
    public float Min_Size;
    Vector3 target_pos;

    private float target_x;
    private float target_y;


    
    bool isControl = false;

    private Dictionary<KeyCode, Vector3> Move_Key = new Dictionary<KeyCode, Vector3>()
    {
        [KeyCode.UpArrow] = new Vector3(0, 1, 0), // 상
        [KeyCode.DownArrow] = new Vector3(0, -1, 0), // 하
        [KeyCode.LeftArrow] = new Vector3(-1, 0, 0), // 좌
        [KeyCode.RightArrow] = new Vector3(1, 0, 0), // 우
    };


    void Awake()
    {
        camera = GetComponent<Camera>();
        cameraTransform = GetComponent<Transform>();
        Max_Size = camera.orthographicSize; //최대 카메라 거리
        Min_Size = 3f; //최소 카메라 거리
        //BasePos = new Vector3(3.5f, 9.2f, -10f);

        /*
        Map1 -> CameraPos = 7, 6.5 -> 9, 4.9
        Map2 -> CameraPos = 8, 2 -> 5.71, 2



        */

        float camera_x = transform.position.x * 0.5f;
        float camera_y = transform.position.y * 1.4f;

        //BasePos = new Vector3(camera_x, camera_y, -10f);

        BasePos = transform.position;
    }



    void Update()
    {
        
        /*
        if(!playerScript.isStop && camera.orthographicSize != Max_Size)
        {
            if(Input.GetKeyDown(KeyCode.UpArrow))
            {
                target_x = transform.position.x;
                target_y = transform.position.y + 1;
                target_pos = new Vector3(target_x, target_y, -10);
                transform.position = Vector3.MoveTowards(transform.position, target_pos, 0.1f);
            }
            
            if(Input.GetKeyDown(KeyCode.DownArrow))
            {
                target_x = transform.position.x;
                target_y = transform.position.y - 1;
                target_pos = new Vector3(target_x, target_y, -10);
                transform.position = Vector3.MoveTowards(transform.position, target_pos, 0.1f);
            }

            if(Input.GetKeyDown(KeyCode.LeftArrow))
            {
                target_x = transform.position.x - 1;
                target_y = transform.position.y;
                target_pos = new Vector3(target_x, target_y, -10);
                transform.position = Vector3.MoveTowards(transform.position, target_pos, 0.1f);
            }

            if(Input.GetKeyDown(KeyCode.RightArrow))
            {
                target_x = transform.position.x + 1;
                target_y = transform.position.y;
                target_pos = new Vector3(target_x, target_y, -10);
                transform.position = Vector3.MoveTowards(transform.position, target_pos, 0.1f);
            }
        }
        */



        if(Input.GetKeyDown(KeyCode.Q) && camera.orthographicSize < Max_Size)
        {
            //Debug.Log("Q");

            camera.orthographicSize += 1f;

            UpdateCamera(true);
        }


        if(Input.GetKeyDown(KeyCode.E) && camera.orthographicSize > Min_Size)
        {
            //Debug.Log("E");

            camera.orthographicSize -= 1f;
        
            UpdateCamera(false);
        }



        
    }

    void UpdateCamera(bool sign) //sign -> true = 줌아웃, false = 줌인
    {
        if(transform.position.z >-1)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        }
        

        int temp = -1;

        Vector3 playerPos = playerScript.transform.position;

        if(sign) playerPos = BasePos;

        Vector3 currentCameraPosition = cameraTransform.position;
        

        Vector3 offsetCamera = playerPos - currentCameraPosition - (playerPos - currentCameraPosition) / (camera.orthographicSize / (camera.orthographicSize + temp));


        currentCameraPosition += offsetCamera;
        
        cameraTransform.position = currentCameraPosition;


    }

            


}
