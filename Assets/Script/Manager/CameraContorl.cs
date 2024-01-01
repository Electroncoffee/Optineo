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
        BasePos = new Vector3(3.5f, 9.2f, -10f);
    }



    void Update()
    {
        // Lerp 혹은 Smoothdamp를 통해 플레이어의 조작에 따라 움직이는 기능.
        // 이 때 플레이어블 캐릭터의 움직임을 제한해야됨. 같은 조작을 통해 움직일 것이기 때문

        // 카메라의 확대, 축소 기능
        // 최대 확대 거리와 최소 축소 거리를 정해야함. 이는 맵 전체를 볼 수 있어야 하며, 픽셀이 깨지지 않을 수준까지 확대 가능해야함
        // 우선 확대 축소의 기능부터 구현해보자



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
        

        int temp = 0;
        if(sign){temp--;} //줌아웃
        else {temp--;} //줌인

        Vector3 playerPos = playerScript.transform.position;

        if(sign) playerPos = BasePos;

        Vector3 currentCameraPosition = cameraTransform.position;
        

        Vector3 offsetCamera = playerPos - currentCameraPosition - (playerPos - currentCameraPosition) / (camera.orthographicSize / (camera.orthographicSize + temp));


        currentCameraPosition += offsetCamera;
        
        cameraTransform.position = currentCameraPosition;


    }

            


}
