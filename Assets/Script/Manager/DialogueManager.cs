using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

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

    //public bool middle;


}

public class DialogueManager : MonoBehaviour //다이얼로그 출력을 위한 클래스
{
    public static DialogueManager instance;
    [SerializeField] Image sprite_L; //왼쪽 캐릭터 스프라이트
    [SerializeField] Image sprite_R; //오른쪽 캐릭터 스프라이트 

    [SerializeField] Text txt_name; //이름 텍스트
    //[SerializeField] Image namepanel;
    //이름 텍스트 패널 (나레이션 텍스트 출력 때에는 SetActive False)
    [SerializeField] Text txt_dialogue; //대화 내용 텍스트

    [SerializeField] float txt_speed;

    [SerializeField] Dialogue[] dialogues; //Dialogue 클래스의 배열화

    

    //bool isDialogue = false;
    int count; //다이얼로그 순서 

    //다이얼로그 창 활성화를 위한 함수... 이지만 다이얼로그 씬을 분리할거라면 필요없다고 생각...
    /*void ONOFF(bool _flag) 
    {
        sprite_L.gameObject.SetActive(_flag);
        sprite_R.gameObject.SetActive(_flag);
        txt_dialogue.gameObject.SetActive(_flag);
        txt_dialogue.gameObject.SetActive(_flag);
        isDialogue = _flag;
    }*/


    private void Awake() //싱글톤 사용해서 어디서든 start 함수를 쓸 수 있게
    {
        if (instance == null) instance = this;

        else if (instance != this) Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }



    public void start_dialogue() //다이얼로그를 시작하는 함수
    {
        //isDialogue = true;
        count = 0;
        next_dialogue();

    }

    public void next_dialogue() //다음 다이얼로그로 넘어가는 함수
    {
        string txt_change = dialogues[count].dialogue;



        //다음 배열로
        txt_name.text = dialogues[count].name;
        txt_dialogue.text = dialogues[count].dialogue;
        sprite_L.sprite = dialogues[count].img_L;
        sprite_R.sprite = dialogues[count].img_R;
        count++;

        StartCoroutine(Typing(txt_change));
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
            if (count < dialogues.Length) next_dialogue();
            //else isDialogue = false;
        }
        //++텍스트가 전부 재생되기 전에 누르면 넘어가지 않음

        /*if (isDialogue)
        {
            
            
        }*/
    }


    IEnumerator Typing(string text)
    {
        txt_dialogue.text = string.Empty;

        StringBuilder stringBuilder = new StringBuilder();

        for (int i = 0; i < text.Length; i++)
        {
            stringBuilder.Append(text[i]);
            txt_dialogue.text = stringBuilder.ToString();

            yield return new WaitForSeconds(txt_speed);
        }
    }

    //텍스트가 한 글자씩 출력되는 애니메이션
    //IEnumerator typing(string text)

}
