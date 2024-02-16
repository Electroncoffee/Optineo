using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


//컷씬 이미지가 하나씩 내려오기(올라가기)
//검게 페이드 아웃/페이드 인


[System.Serializable]
public class Dialogue //다이얼로그 요소 정보를 담은 클래스
{
    public string name; //캐릭터의 이름

    [TextArea] //여러 줄 쓸 수 있게 해줌
    public string dialogue; //대화 내용

    public Sprite img_L; //왼쪽 캐릭터의 스탠딩 
    public Sprite img_R; //오른쪽 캐릭터의 스탠딩

    //public bool panel_Name; //이름 텍스트 패널
    public Image panel_dialogue; //다이얼로그 텍스트 패널
    public Sprite dl_background;

    public Sprite cutScene;

    public bool fade;

    //public bool middle;


}

public class DialogueManager : MonoBehaviour //다이얼로그 출력을 위한 클래스
{
    public static DialogueManager instance;

    [SerializeField] GameObject visible; 

    [SerializeField] Image sprite_L; //왼쪽 캐릭터 스프라이트
    [SerializeField] Image sprite_R; //오른쪽 캐릭터 스프라이트 

    [SerializeField] Text txt_name; //이름 텍스트
    //[SerializeField] Image namepanel; //이름 텍스트 패널
    [SerializeField] Text txt_dialogue; //대화 내용 텍스트

    [SerializeField] Image background; //배경

    [SerializeField] Image cutscene; //컷신

    [SerializeField] CanvasRenderer canvasRenderer;

    [SerializeField] float txt_speed;

    [SerializeField] Dialogue[] dialogues; //Dialogue 클래스의 배열화

    


    public bool istyping;

    string currentText;

    
    int count; //다이얼로그 순서 

    


    private void Awake() //싱글톤 사용해서 어디서든 start 함수를 쓸 수 있게
    {
        if (instance == null) instance = this;

        else if (instance != this) Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        visible.SetActive(false);
    }



    public void start_dialogue() //다이얼로그를 시작하는 함수
    {
        
        count = 0;
        next_dialogue();

        visible.SetActive(true);

    }

    public void next_dialogue() //다음 다이얼로그로 넘어가는 함수
    {
        //원하는 부분에서 1초동안 딜레이 후 SetActive(false), 1초 동안 딜레이 후 SetActive(true)
        //원하는 부분에서 StartCoroutine(Fade()); 실행

        //원하는 부분에서 SetActive(false) 멈췄다가  cutsceneAnimator 실행이 끝난 뒤 다음 다이얼로그로 넘어가기


        if (istyping)
        {
            StopAllCoroutines();
            txt_dialogue.text = currentText;
            istyping = false;
        }
        
        else if (!istyping && count < dialogues.Length)
        {
            string txt_change = dialogues[count].dialogue;

            //다음 배열로
            txt_name.text = dialogues[count].name;
            txt_dialogue.text = dialogues[count].dialogue;
            sprite_L.sprite = dialogues[count].img_L;
            sprite_R.sprite = dialogues[count].img_R;
            background.sprite = dialogues[count].dl_background;
            cutscene.sprite = dialogues[count].cutScene;

            if (dialogues[count].img_L == null)
            {
                sprite_L.gameObject.SetActive(false);
            }
            else
            {
                sprite_L.gameObject.SetActive(true);
            }

            if (dialogues[count].img_R == null)
            {
                sprite_R.gameObject.SetActive(false);
            }
            else
            {
                sprite_R.gameObject.SetActive(true);
            }

            if (dialogues[count].cutScene == null)
            {
                cutscene.gameObject.SetActive(false);
            }
            else
            {
                cutscene.gameObject.SetActive(true);
            }

            count++;

            StartCoroutine(Typing(txt_change));
        }
        else
        {
            visible.SetActive(false);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        start_dialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (istyping)
            {
                StopAllCoroutines();
                txt_dialogue.text = currentText;
                istyping = false;
            }
            else
            {
                if (dialogues[count].fade == true)//fade가 체크되어 있으면 클릭을 눌렀을 때 페이드 아웃 실행
                {
                    FadeInOut.instance.Transition(); //페이드 인 아웃하는 코루틴
                    StartCoroutine(dl_Transition()); //다이얼로그를 멈췄다가 실행하는 코루틴
                    //코루틴 실행중에는 클릭이 작동하지 않도록
                }
                else
                {
                    next_dialogue();
                }
            }
        }
    }
    IEnumerator dl_Transition()
    {
        yield return new WaitForSeconds(1);
        visible.SetActive(false);

        yield return new WaitForSeconds(2);
        visible.SetActive(true);

        next_dialogue();
    }
    IEnumerator Typing(string text)
    {
        txt_dialogue.text = string.Empty;
        currentText = text;

        StringBuilder stringBuilder = new StringBuilder();

        for (int i = 0; i < text.Length; i++)
        {
            stringBuilder.Append(text[i]);
            txt_dialogue.text = stringBuilder.ToString();

            yield return new WaitForSeconds(txt_speed);
            istyping = true;
        }
        istyping = false;
    }
}
