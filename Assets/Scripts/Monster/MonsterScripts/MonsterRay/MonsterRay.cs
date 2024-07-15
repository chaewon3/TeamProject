using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterRay : MonoBehaviour
{
    // 그냥 xz 회전 잠구고 캡슐 콜라이더 붙이면 어느정도 경사는 넘어감
    // 떨어짐 방지로 써볼까 했지만 떨어질만한 곳이 없음
    // 만들긴 했는데 쓸모는 없어짐 - 2024-07-15




    #region 전역 변수
    //public
    // 레이의 길이
    public float rayDistance = 0.5f;

    // 레이가 시작할 위치
    public float rayForwardTransform = 0.5f;    // 앞으로 얼마나 갈건지
    public float rayUpTransform = 0.5f;         // 위로 얼마나 갈건지

    // 어느 레이어와 상호작용할건지
    public LayerMask layerMask;

    // 보간의 속도 (높을수록 보간 속도 빠름)
    public float lerpSpeed = 10.0f;


    //private
    // 레이가 활성화되었을 때 체크하는 주기의 속도
    float checkInterval = 0.05f;

    // stop,start coroutine을 위한 변수
    Coroutine coroutine;

    // 지역변수로 생성하는 것보다 생성해두고 할당하는 것이 더 좋아보여서
    Ray ray;


    Vector3 rayPosition;
    #endregion

    private void Start()
    {
        StartRayCastCoroutine();
    }




    // 움직일때만 활성화 하기
    void StartRayCastCoroutine()
    {
        if (coroutine == null)
        {
            coroutine = StartCoroutine(RaycastRoutine());
        }
    }

    // 움직임을 멈추면 비활성화하기
    void StopRayCastCoroutine()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
    }

    IEnumerator RaycastRoutine()
    {
        while (true)
        {
            ShootRayDownward();

            yield return new WaitForSeconds(checkInterval);
        }
    }

    void ShootRayDownward()
    {
        rayPosition = transform.position + transform.forward * rayForwardTransform + transform.up * rayUpTransform;

        ray = new Ray(rayPosition, Vector3.down);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDistance, layerMask))
        {
            float distanceToGround = hit.distance;
            float targetYPosition = transform.position.y + (0.5f - distanceToGround);
            Vector3 targetPosition = new Vector3(transform.position.x, targetYPosition, transform.position.z);

            // t가 클수록 빠름
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * lerpSpeed);

            print(1);
        }
        else
        {
            print(2);
        }
    }
}
