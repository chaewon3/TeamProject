using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Events;

public class MineCartMove : MonoBehaviour, IInteractable
{
    public GameObject MineCart;
    public GameObject MineCartCollapse;
    public Transform lever;
    public CinemachineVirtualCamera cutScenecam;
    bool isTrigger;
    float speed = 5f;

    private void Update()
    {
        // todo ��ȣ�ۿ�Ű �÷��̾�� �ű�
        if(Input.GetKeyDown(KeyCode.G) && !isTrigger)
        {
            lever.Rotate(-104, 0, 0);
            StartCoroutine(LeverOn());
            isTrigger = true;
        }
    }
    /// <summary>
    /// ���� �ִϸ��̼ǰ� Virtual Camera �̺�Ʈ �ƾ� ����
    /// </summary>
    IEnumerator LeverOn()
    {
        cutScenecam.Priority = 11;
        yield return new WaitForSeconds(2f);
        float time = 0;
        while (time <= 2.5f)
        {
            if (time >= 0.5f)
            {
                cutScenecam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().enabled = false;
            }
            MineCart.transform.Translate(-MineCart.transform.forward * speed * Time.deltaTime);
            time += Time.deltaTime;
            yield return null;
        }
        cutScenecam.enabled = false;
        MineCartCollapse.SetActive(true);
        Destroy(MineCart, 0);
    }

    public void interaction()
    {
        if (!isTrigger)
        {
            lever.Rotate(-104, 0, 0);
            StartCoroutine(LeverOn());
            isTrigger = true;
        }
    }
}
