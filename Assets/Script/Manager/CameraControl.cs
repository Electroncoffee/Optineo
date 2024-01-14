using System.Collections;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

    public PlayerMove playerScript;
    private Camera camerainfo;
    private float target_size;
    private Vector3 BasePos; //원래 상태로 복귀하기 위한 벡터
    Transform cameraTransform;


    public float Max_Size;
    public float Min_Size;
    public float smoothTime;
    public float zoomspeed; //확대, 축소되며 화면이 이동하는 속도를 조절. 너무 부족할 경우 목표 위치에 도달하지 못할 수 있음
    public float min_tile_x; //맵 최소 x 좌표
    public float max_tile_x; //맵 최대 x 좌표
    public float min_tile_y; //맵 최소 y 좌표
    public float max_tile_y; //맵 최대 y 좌표
    private Vector3 velocity = Vector3.zero;
    private float temp;

    public bool isActive = true;
    public bool isControl = false;
    public bool isTracking = false;
    public bool isZoom = false;
    public bool isZoomIn = false;

    Vector3 up_pos = new Vector3(0, 0.1f, 0);
    Vector3 down_pos = new Vector3(0, -0.1f, 0);
    Vector3 left_pos = new Vector3(-0.1f, 0, 0);
    Vector3 right_pos = new Vector3(0.1f, 0, 0);
    Vector3 target_pos;
    Vector3 temppos; //카메라 무빙 이전의 좌표를 기억하기 위한 변수




    void Awake()
    {
        camerainfo = GetComponent<Camera>();
        cameraTransform = GetComponent<Transform>();
        Max_Size = camerainfo.orthographicSize; //최대 카메라 거리
        Min_Size = 3f; //최소 카메라 거리

        BasePos = transform.position;
        target_pos = transform.position;



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
        if(Input.GetKey(KeyCode.LeftControl) && !isZoom)
        {
            if(!isControl)
            {
                temppos = transform.position; //카메라 무빙 시작 지점 저장
                isControl = true;
            }
            playerScript.isActing = false;
            if(!playerScript.isActing && isControl)
            {
                if(Input.GetKey(KeyCode.UpArrow))
                {
                    target_pos += up_pos;
                }

                if(Input.GetKey(KeyCode.DownArrow))
                {
                    target_pos += down_pos;
                }

                if(Input.GetKey(KeyCode.LeftArrow))
                {
                    target_pos += left_pos;
                }

                if(Input.GetKey(KeyCode.RightArrow))
                {
                    target_pos += right_pos;
                }
                

                if(transform.position != target_pos)
                {
                    transform.position = Vector3.SmoothDamp(transform.position, target_pos, ref velocity, Time.deltaTime * smoothTime);
                }
                
            }
        }
        else if(Input.GetKeyUp(KeyCode.LeftControl) && isControl)
        {
            isActive = false;
            StartCoroutine(TrasnPos(temppos));
        }
        
    }


    void TrackingPlayer() //플레이어를 추적하는 기능
    {

        //조건 1 Zoom Out 상태에서도 카메라의 중앙을 넘어설 경우 캐릭터를 추적
        //조건 1-1 Zoom Out 상태일 경우 맵의 최소 거리와 최대 거리에 대한 정지 위치가 다름 (씬2와 같은 경우 카메라가 움직이지 않음)
        //조건 1-2 즉 카메라 Zoom Out 상태인 경우 카메라가 중앙을 넘는 경우에도 minTile+maxtile > 30 이 아닌 경우 추적 X
        //조건 2 Zoom In 상태일 경우 카메라의 중앙을 넘어설 경우 캐릭터를 추적

        //조건 3 카메라는 min_tile_x +5, max_tile_y-5 에 도달할 경우 정지 (추적 정지)
        //조건 4 카메라가 min_tile_x +5, max_tile_y-5 영역에서 탈출할 경우 재추적
        //조건 3,4 에 대한 상황은 Zoom In 상황

        if(isZoomIn)
        {

            if(isTracking)
            {
                if(transform.position.x < min_tile_x + 5)
                {
                    isTracking = false;
                }

                if(transform.position.x > max_tile_x - 5)
                {
                    isTracking = false;
                }
            }
            else
            {
                if(playerScript.transform.position.x > min_tile_x + 5)
                {
                    if(playerScript.transform.position.x < max_tile_x - 5)
                    {
                        isTracking = true;
                    }
                }
            }

        }
        else
        {
            if(max_tile_x + min_tile_x > 30) //맵의 x 크기가 30이상이 아닐 경우 줌 아웃 상태에서 추적 X
            {
                if(Mathf.Approximately(transform.position.x, playerScript.transform.position.x))
                {
                    isTracking = true;
                }
            }
            else
            {
                isTracking = false;
            }
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
        if(Input.GetKeyDown(KeyCode.Q) && camerainfo.orthographicSize < Max_Size && !isZoom) //줌 아웃
        {
            StartCoroutine(UpdateCamera(false));
            isZoom = true;
            isTracking = false;
            playerScript.isActing = false;
        }


        if(Input.GetKeyDown(KeyCode.E) && camerainfo.orthographicSize > Min_Size && !isZoom) //줌 인
        {
            StartCoroutine(UpdateCamera(true));
            isZoom = true;
            playerScript.isActing = false;
        }
    }



    IEnumerator UpdateCamera(bool sign) //sign -> true = 줌 인, false = 줌 아웃
    {
        while(true)
        {
            float size;
            if(sign) size = Min_Size;
            else size = Max_Size;

            Vector3 playerPos = playerScript.transform.position;
            if(playerPos.x <= min_tile_x + Min_Size) //만약 맵의 최소 타일과 카메라의 최소 사이즈를 더한 값보다 플레이어가 멀다면
            {
                playerPos.x = min_tile_x + 5;
            }
            else if(playerPos.x >= max_tile_x - Min_Size) //만약 맵의 최대 타일과 카메라의 최소 사이즈를 뺀 값보다 플레이어가 멀다면
            {
                playerPos.x = max_tile_x - 5;
            }

            if(playerPos.y <= min_tile_y + 2)
            {
                playerPos.y = min_tile_y + 2;
            }
            else if(playerPos.y >= max_tile_y - 2)
            {
                playerPos.y = max_tile_y - 2;
            }


            //카메라가 조절될 사이즈를 Mathf 를 통해 계산 및 설정
            target_size = Mathf.SmoothDamp(camerainfo.orthographicSize, size, ref temp, Time.deltaTime * 60f);
            target_size = Mathf.Clamp(target_size, Min_Size, Max_Size);

            //현재 카메라의 위치를 저장
            Vector3 currentCameraPosition = cameraTransform.position;  

            //아래에서 경우에 따라 계산되어 현재 카메라의 위치와 더할 변수를 설정
            Vector3 offsetCamera;

            //확대 시에는 플레이어를 기준으로 카메라의 위치를 통해 offset값을 조절
            //축소 시에는 현재 카메라의 위치를 기준으로 기초 카메라 위치를 통해 offset값을 조절
            if(sign) offsetCamera = playerPos - currentCameraPosition - (playerPos - currentCameraPosition) / (camerainfo.orthographicSize / target_size);
            else offsetCamera = currentCameraPosition - BasePos - (currentCameraPosition - BasePos) / ( camerainfo.orthographicSize/target_size);
            
            //2D 환경에서 카메라의 z값이 0이하로 내려가면 오브젝트들이 보이지 않으므로 offset의 z값을 0으로 조절해두기
            offsetCamera.z = 0f;

            camerainfo.orthographicSize = target_size;

            currentCameraPosition += zoomspeed * offsetCamera;
            
            cameraTransform.position = currentCameraPosition;



            if(Mathf.Approximately(camerainfo.orthographicSize, Max_Size))
            {
                isZoom = false;
                playerScript.isActing = true;
                isZoomIn = false;
                target_pos = transform.position;
                yield break;
            }
            else if(Mathf.Approximately(camerainfo.orthographicSize, Min_Size))
            {
                isZoom = false;
                playerScript.isActing = true;
                isZoomIn = true;
                target_pos = transform.position;
                yield break;
            }

            yield return null;
        }
    }

    IEnumerator TrasnPos(Vector3 target) //카메라의 좌표를 target으로 보내는 코루틴
    {
        while(true)
        {
            transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, Time.deltaTime * smoothTime);
            if(Mathf.Approximately(transform.position.x,target.x) && Mathf.Approximately(transform.position.y, target.y))
            {
                isControl = false;
                isActive = true;
                playerScript.isActing = true;
                target_pos = target;
                yield break;
            }
            yield return null;
        }
    }


}
