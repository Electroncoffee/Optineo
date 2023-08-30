using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Shake : MonoBehaviour
{
    private float shakeAmount = 0.05f; // 흔들림의 강도
    private float shakeDuration = 0.2f; // 흔들림 지속 시간

    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.position;
    }

    public void StartShake()
    {
        StartCoroutine(Shaking());
    }

    private IEnumerator Shaking()
    {
        float elapsedTime = 0f;
        while (elapsedTime < shakeDuration)
        {
            Vector3 randomOffset = Random.insideUnitSphere * shakeAmount;
            transform.position = initialPosition + randomOffset;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = initialPosition;
    }
}