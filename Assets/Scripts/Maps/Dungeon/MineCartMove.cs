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
        // todo 상호작용키 플레이어로 옮김
        if(Input.GetKeyDown(KeyCode.G) && !isTrigger)
        {
            lever.Rotate(-104, 0, 0);
            StartCoroutine(LeverOn());
            isTrigger = true;
        }
    }
    /// <summary>
    /// 광차 애니메이션과 Virtual Camera 이벤트 컷씬 제어
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
