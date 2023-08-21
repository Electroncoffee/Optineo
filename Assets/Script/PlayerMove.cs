using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Vector3 Player_Start_Pos; //�̵��� ��� ��ǥ
    private Vector3 target_pos; //�̵��� ��ǥ
    private Vector3 delta_pos; //����ġ ��ǥ
    Dictionary<KeyCode, Vector3> Move_Key;
    public float moveSpeed; // �̵� �ӵ�
    public float moveDistance; // �̵� �Ÿ�
    public bool isStop = true; // ���� ����
    void Start()
    {
        Move_Key = new Dictionary<KeyCode, Vector3>();
        Move_Key[KeyCode.UpArrow] = new Vector3(0, moveDistance, 0); ; // ��
        Move_Key[KeyCode.DownArrow] = new Vector3(0, -moveDistance, 0); ; // ��
        Move_Key[KeyCode.LeftArrow] = new Vector3(-moveDistance, 0, 0); ; // ��
        Move_Key[KeyCode.RightArrow] = new Vector3(moveDistance, 0, 0); ; // ��
    }
    void Update()
    {
        if (isStop)
        {
            foreach (KeyValuePair<KeyCode, Vector3> item in Move_Key)
            {
                if (Input.GetKeyDown(item.Key))
                {
                    isStop = false;
                    Player_Start_Pos = transform.position;
                    target_pos = Player_Start_Pos + (item.Value * moveDistance);
                }
            }
        }
    }
    private void FixedUpdate()
    {
        if (!isStop)
            transform.position = Vector3.MoveTowards(transform.position, target_pos, Time.deltaTime * moveSpeed);
        if (transform.position.Equals(target_pos))
            isStop = true;
    }
}

