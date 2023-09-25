using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floating : MonoBehaviour
{
    public float moveDistance; // 움직일 거리
    public float moveSpeed;    // 움직이는 속도
    public float moveDelay;    // 움직임 간격

    private Vector3 initialPosition;
    private Vector3 targetPosition;
    private bool movingUp = true;
    void Start()
    {
        initialPosition = transform.position;
        targetPosition = initialPosition + Vector3.up * moveDistance;
        StartCoroutine(MoveObject());
    }
    private IEnumerator MoveObject()
    {
        while (true)
        {
            while (transform.position != targetPosition)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * moveSpeed);
                yield return null;
            }

            // 대기 시간
            yield return new WaitForSeconds(moveDelay);

            // 방향 변경
            if (movingUp)
            {
                targetPosition = initialPosition;
            }
            else
            {
                targetPosition = initialPosition + Vector3.up * moveDistance;
            }

            movingUp = !movingUp;
        }
    }
}