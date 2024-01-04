using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

    public PlayerMove playerScript;
    private Camera camera;
    private Vector3 BasePos; //원래 상태로 복귀하기 위한 벡터
    Transform cameraTransform;


    public float Max_Size;
    public float Min_Size;
    public float smoothTime;
    private Vector3 velocity = Vector3.zero;

    public bool isControl = false;

    Vector3 up_pos = new Vector3(0, 3, 0);
    Vector3 down_pos = new Vector3(0, -3, 0);
    Vector3 left_pos = new Vector3(-3, 0, 0);
    Vector3 right_pos = new Vector3(3, 0, 0);
    Vector3 target_pos;




    void Awake()
    {
        camera = GetComponent<Camera>();
        cameraTransform = GetComponent<Transform>();
        //Max_Size = camera.orthographicSize; //최대 카메라 거리
        //Min_Size = 3f; //최소 카메라 거리

        BasePos = transform.position;
        target_pos = BasePos;

    }



    void Update()
    {
        ControlCamera();
    }

    void ControlCamera()
    {
        if(Input.GetKey(KeyCode.LeftControl))
        {
            isControl = true;
            playerScript.isActing = false;
            if(!playerScript.isActing && isControl)
            {

                if(Input.GetKeyDown(KeyCode.UpArrow))
                {
                    //Debug.Log("UP");
                    target_pos += up_pos;
                }
                else if(Input.GetKeyUp(KeyCode.UpArrow))
                {
                    target_pos -= up_pos;
                }

                if(Input.GetKeyDown(KeyCode.DownArrow))
                {
                    //Debug.Log("UP");
                    target_pos += down_pos;
                }
                else if(Input.GetKeyUp(KeyCode.DownArrow))
                {
                    target_pos -= down_pos;
                }

                if(Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    //Debug.Log("UP");
                    target_pos += left_pos;
                }
                else if(Input.GetKeyUp(KeyCode.LeftArrow))
                {
                    target_pos -= left_pos;
                }

                if(Input.GetKeyDown(KeyCode.RightArrow))
                {
                    //Debug.Log("UP");
                    target_pos += right_pos;
                }
                else if(Input.GetKeyUp(KeyCode.RightArrow))
                {
                    target_pos -= right_pos;
                }
                
                if(transform.position != target_pos)
                {
                    transform.position = Vector3.SmoothDamp(transform.position, target_pos, ref velocity, Time.deltaTime * smoothTime);
                }
                
            }
        }
        else if(Input.GetKeyUp(KeyCode.LeftControl))
        {
            isControl = false;
            playerScript.isActing = true;

            transform.position = BasePos;
        
        }
        
    }


    void ZoomCamera() //카메라를 줌, 아웃하는 기능. 업데이트에 넣어서 사용
    {
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
