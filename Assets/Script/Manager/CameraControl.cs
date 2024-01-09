using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraControl : MonoBehaviour
{

    public PlayerMove playerScript;
    private Camera camera;
    private float camera_size;
    private Vector3 BasePos; //원래 상태로 복귀하기 위한 벡터
    Transform cameraTransform;


    public float Max_Size;
    public float Min_Size;
    public float smoothTime;
    public float min_tile_x; //맵 최소 x 좌표
    public float max_tile_x; //맵 최대 x 좌표
    private Vector3 velocity = Vector3.zero;

    public bool isActive = true;
    public bool isControl = false;
    public bool isTracking = false;

    Vector3 up_pos = new Vector3(0, 3, 0);
    Vector3 down_pos = new Vector3(0, -3, 0);
    Vector3 left_pos = new Vector3(-3, 0, 0);
    Vector3 right_pos = new Vector3(3, 0, 0);
    Vector3 target_pos;




    void Awake()
    {
        camera = GetComponent<Camera>();
        cameraTransform = GetComponent<Transform>();
        camera_size = camera.orthographicSize;
        Max_Size = camera.orthographicSize; //최대 카메라 거리
        Min_Size = 3f; //최소 카메라 거리

        BasePos = transform.position;
        target_pos = BasePos;



    }



    void Update()
    {
        if(isActive) //카메라 기능이 필요 없는 스테이지의 경우 isActive를 통해 활성화-비활성화
        {
            //화면 이동 조작
            ControlCamera();

            //화면 중앙 이탈 시 플레이어 추적
            TrackingPlayer();

            //화면 줌인-줌아웃
            ZoomCamera();
        }
    }

    void ControlCamera() //카메라를 상하좌우로 움직이는 기능
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
            playerScript.flag_isActing(true, 1.0f);

            transform.position = BasePos;
        }
        
    }


    void TrackingPlayer() //플레이어를 추적하는 기능
    {
        if(playerScript.transform.position.x <= min_tile_x + 5) //트래킹 활성화
        {
            isTracking = false;
        }
        else if(playerScript.transform.position.x >= max_tile_x - 5) //오른쪽
        {
            isTracking = false;
        }
        else
        {
            isTracking = true;
        }

        

        if(isTracking && !isControl)
        {
            if(playerScript.transform.position != transform.position && isTracking) //캐릭터 추적
            {
                Vector3 tracking_pos = new Vector3(playerScript.transform.position.x, transform.position.y, transform.position.z);
                transform.position = Vector3.SmoothDamp(transform.position, tracking_pos, ref velocity, Time.deltaTime * smoothTime);
            }
            target_pos = transform.position;
        }


    }


    void ZoomCamera() //카메라를 줌, 아웃하는 기능. 업데이트에 넣어서 사용
    {
        if(Input.GetKeyDown(KeyCode.Q) && camera.orthographicSize < Max_Size) //줌 아웃
        {
            //Debug.Log("Q");
            
            //코루틴을 사용해 구현할 것 - 업데이트를 통해 작동하기 때문에 for문 등의 반복문에 대해서 과도한 사용횟수가 발생 -메모리 과부하
            

            //StartCoroutine(ZoomControl(true));
            

            camera.orthographicSize += 1f;

            UpdateCamera(true);
        }


        if(Input.GetKeyDown(KeyCode.E) && camera.orthographicSize > Min_Size) //줌 인
        {
            //Debug.Log("E");

            //코루틴을 사용해 구현할 것
            
            
            ///StartCoroutine(ZoomControl(false));



            camera.orthographicSize -= 1f;
        
            UpdateCamera(false);
        }
    }

    void UpdateCamera(bool sign) //sign -> true = 줌인, false = 줌아웃
    {
        if(transform.position.z > -1)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        }
        

        int temp = 1;

        Vector3 playerPos = playerScript.transform.position;

        if(sign) playerPos = BasePos;

        Vector3 currentCameraPosition = cameraTransform.position;
        Vector3 offsetCamera = playerPos - currentCameraPosition - (playerPos - currentCameraPosition) / (camera.orthographicSize + temp / camera.orthographicSize);

        camera.orthographicSize += 1f;

        currentCameraPosition += offsetCamera;
        
        cameraTransform.position = currentCameraPosition;


    }

    IEnumerator ZoomControl(bool IsZoom)
    {

        //int integer = 0;
        float target_size = 0f;

        if(IsZoom == true) //확대
        {
            //integer += 1;
            target_size = Min_Size;
        }
        else if(IsZoom == false) //축소
        {
            //integer -= 1;
            target_size = Max_Size;
        }

        for(int i = 0; i < 50; i++)
        {
            camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, target_size, Time.deltaTime * 50f);
            camera.orthographicSize = Mathf.Clamp(camera.orthographicSize, Min_Size, Max_Size);

            UpdateCamera(true);
        }





        yield return null; 
    }
            


}
