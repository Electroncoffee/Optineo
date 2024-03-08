using UnityEngine;

public class minion : MonoBehaviour
{

    

    void killed()
    {
        //보스의 특정 패턴에 의한 사망
        Destroy(gameObject);
    }
}
