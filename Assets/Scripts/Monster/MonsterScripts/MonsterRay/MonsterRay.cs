using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterRay : MonoBehaviour
{
    // �׳� xz ȸ�� �ᱸ�� ĸ�� �ݶ��̴� ���̸� ������� ���� �Ѿ
    // ������ ������ �Ẽ�� ������ ���������� ���� ����
    // ����� �ߴµ� ����� ������ - 2024-07-15




    #region ���� ����
    //public
    // ������ ����
    public float rayDistance = 0.5f;

    // ���̰� ������ ��ġ
    public float rayForwardTransform = 0.5f;    // ������ �󸶳� ������
    public float rayUpTransform = 0.5f;         // ���� �󸶳� ������

    // ��� ���̾�� ��ȣ�ۿ��Ұ���
    public LayerMask layerMask;

    // ������ �ӵ� (�������� ���� �ӵ� ����)
    public float lerpSpeed = 10.0f;


    //private
    // ���̰� Ȱ��ȭ�Ǿ��� �� üũ�ϴ� �ֱ��� �ӵ�
    float checkInterval = 0.05f;

    // stop,start coroutine�� ���� ����
    Coroutine coroutine;

    // ���������� �����ϴ� �ͺ��� �����صΰ� �Ҵ��ϴ� ���� �� ���ƺ�����
    Ray ray;


    Vector3 rayPosition;
    #endregion

    private void Start()
    {
        StartRayCastCoroutine();
    }




    // �����϶��� Ȱ��ȭ �ϱ�
    void StartRayCastCoroutine()
    {
        if (coroutine == null)
        {
            coroutine = StartCoroutine(RaycastRoutine());
        }
    }

    // �������� ���߸� ��Ȱ��ȭ�ϱ�
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

            // t�� Ŭ���� ����
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * lerpSpeed);

            print(1);
        }
        else
        {
            print(2);
        }
    }
}
