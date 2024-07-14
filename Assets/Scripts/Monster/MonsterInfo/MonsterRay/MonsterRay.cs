using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterRay : MonoBehaviour
{
    public float rayDistance = 1.0f;
    public float checkInterval = 0.2f;
    public LayerMask layerMask;

    public float rayForwardTransform = 0.5f;
    public float rayUpTransform = 0.5f;

    public float lerpSpeed = 10.0f;


    // 움직일 때만 이걸 키면 될 것 같음
    private void Start()
    {
        //StartCoroutine(RaycastRoutine());
    }

    private void Update()
    {
        ShootRayDownward();
    }

    IEnumerator RaycastRoutine()
    {
        while (true)
        {
            ShootRayDownward();

            // 일정 시간 대기 후 다시 실행
            yield return new WaitForSeconds(checkInterval);
        }
    }

    void ShootRayDownward()
    {
        Vector3 rayPosition = transform.position + transform.forward * rayForwardTransform + transform.up * rayUpTransform;

        Ray ray = new Ray(rayPosition, Vector3.down);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDistance, layerMask))
        {
            float distanceToGround = hit.distance;
            float targetYPosition = transform.position.y + (0.5f - distanceToGround);
            Vector3 targetPosition = new Vector3(transform.position.x, targetYPosition, transform.position.z);

            // t가 클수록 빠름
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * lerpSpeed);
        }
        else
        {
            Debug.Log(2);
        }
    }

    void OnDrawGizmos()
    {
        Vector3 offsetPosition = transform.position + transform.forward * 1.0f;

        Ray ray = new Ray(offsetPosition, Vector3.down);
        Vector3 endPosition = offsetPosition + Vector3.down * rayDistance;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(ray.origin, endPosition);
    }
}
