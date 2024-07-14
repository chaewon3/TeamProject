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


    // ������ ���� �̰� Ű�� �� �� ����
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

            // ���� �ð� ��� �� �ٽ� ����
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

            // t�� Ŭ���� ����
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
