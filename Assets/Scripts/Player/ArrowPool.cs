using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPool : MonoBehaviour
{
    #region 전역 변수
    public GameObject prefab;
    public Transform spawnPoint;
    public GameObject spawnObj;
    
    List<GameObject> pool = new List<GameObject>();
    int count;
    int poolSize = 12;
    #endregion

    void Awake()
    {
        for(int i =0 ; i < poolSize; i++)
        {
            GameObject arrowObj = Instantiate(prefab, spawnObj.transform);
            arrowObj.SetActive(false);
            pool.Add(arrowObj);
        }
    }

    public GameObject GetObj()
    {
        if (count != pool.Count)
        {
            GameObject arrow = pool[count];
            print($"{count}번 화살 : {pool[count].transform.position}, {pool[count].transform.rotation}");
            arrow.transform.position = spawnPoint.position;
            arrow.transform.rotation = spawnPoint.rotation;
            arrow.SetActive(true);
            arrow.transform.SetParent(null);
            count++;
            return arrow;
        }
        else
        {
            count = 0;
            GameObject arrow = pool[count];
            arrow.transform.position = spawnPoint.position;
            arrow.transform.rotation = spawnPoint.rotation;
            arrow.SetActive(true);
            arrow.transform.SetParent(null);
            count++;
            return arrow;
        }
    }

    public void ReturnObj(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.SetParent(spawnObj.transform);
    }
}
