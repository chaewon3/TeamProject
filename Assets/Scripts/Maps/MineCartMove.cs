using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Events;

public class MineCartMove : MonoBehaviour
{
    public GameObject MineCart;
    public GameObject MineCartCollapse;
    public Transform lever;
    public CinemachineVirtualCamera cutScenecam;
    bool isTrigger;
    float speed = 5f;

    private void Update()
    {
        // 트리커 캐릭터로 옮기면서 컷씬 진행동안 플레이어 움직임 막기
        if(Input.GetKeyDown(KeyCode.G) && !isTrigger)
        {
            lever.Rotate(-104, 0, 0);
            StartCoroutine(LeverOn());
            isTrigger = true;
        }
    }
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
}
