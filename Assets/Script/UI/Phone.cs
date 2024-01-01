using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Phone : MonoBehaviour
{

    /*public RectTransform image_Phone;
    public RectTransform image_SmallPhone;
    public Vector2 target1_Phone;
    public Vector2 target2_Phone;
    public Vector2 target1_SmallPhone;
    public Vector2 target2_SmallPhone;
    public float lerpSpeed = 1.0f;*/

    public Animator anim_Phone;
    public Animator anim_SmallPhone;
    public Animator fade;

    bool code;
    bool isInputblock = false;

    public Button bt;

    public void Start()
    {
        bt = GameObject.Find("Button_Setting").GetComponent<Button>();

        //image_Phone = transform.Find("Phone").GetComponent<RectTransform>();
        //image_SmallPhone = transform.Find("Phone_small").GetComponent<RectTransform>();
        
        code = false;
    }

    public void Update()
    {
        if (!isInputblock)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {



                if (!code)
                {
                    Click();

                    StartCoroutine(On_Phone());

                    Debug.Log("E");
                    code = true;
                    StartCoroutine(KeyBlock(2f));
                }



                else
                {
                    StartCoroutine(Off_Phone());

                    code = false;
                    StartCoroutine(KeyBlock(2f));
                }


            }
        }

        

        
        
    }

    void Click()
    {
        //bt.onClick.Invoke();
    }

    IEnumerator KeyBlock(float seconds)
    {
        isInputblock = true;
        yield return new WaitForSeconds(seconds);
        isInputblock = false;
    }


    IEnumerator On_Phone ()
    {
        anim_SmallPhone.Play("Off_SmallPhone");

        yield return new WaitForSeconds(0.5f);

        anim_Phone.Play("On_Phone");
        fade.Play("On_Background");

        //yield return null;

        /*Vector2 startSmallPhonePosition = image_SmallPhone.anchoredPosition;
        Vector2 startPhonePosition = image_Phone.anchoredPosition;

        while (Vector2.Distance(startSmallPhonePosition, target1_SmallPhone) > 0.01f)
        {
            image_SmallPhone.anchoredPosition
                    = Vector2.Lerp(startSmallPhonePosition, target1_SmallPhone, Time.deltaTime * lerpSpeed);
            
            yield return null;
        }

        yield return new WaitForSeconds(1f);

        while (Vector2.Distance(startPhonePosition, target1_Phone) > 0.01f)
        {
            image_Phone.anchoredPosition
                    = Vector2.Lerp(startPhonePosition, target1_Phone, Time.deltaTime * lerpSpeed);

            yield return null;
        }*/




    }

    IEnumerator Off_Phone()
    {
        anim_Phone.Play("Off_Phone");
        fade.Play("Off_Background");

        yield return new WaitForSeconds(0.5f);

        anim_SmallPhone.Play("On_SmallPhone");

        //yield return null;
        
        /*while (Vector2.Distance(image_Phone.anchoredPosition, target2_Phone) > 0.01f)
        {
            image_Phone.anchoredPosition
                    = Vector2.Lerp(image_Phone.anchoredPosition, target2_Phone, Time.deltaTime * lerpSpeed);
            
            yield return null;
        }

            
      

        yield return new WaitForSeconds(1f);


        while (Vector2.Distance(image_Phone.anchoredPosition, target2_SmallPhone) > 0.01f)
        {
            image_SmallPhone.anchoredPosition
                    = Vector2.Lerp(image_SmallPhone.anchoredPosition, target2_SmallPhone, Time.deltaTime * lerpSpeed);
            
            yield return null;

        }*/


    }
    //E를 눌렀을 때 실행될 함수
    //작은 휴대폰이 내려가는 애니메이션 실행 후  바로 이어서 큰 휴대폰 등장 애니메이션 실행
    //큰 휴대폰 등장 애니메이션 + 검은 배경 페이드 인 실행
    //작은 휴대폰 스크립트에서 애니메이션 재생 후 트리거 함수 실행


}
