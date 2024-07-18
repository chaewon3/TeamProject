using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPooling : MonoBehaviour
{
    #region 전역 변수
    public GameObject prefab;
    public Transform spawnPoint;
    public GameObject spawnObj;
    
    List<GameObject> pool = new List<GameObject>();
    int count;
    int poolSize = 12;

    Vector3 testPoint;
    Vector3 testRota;
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

    void Start()
    {
        testPoint = new Vector3(48.14f, 3.86f, -1.52f);
        testRota = new Vector3(270f, 180f, 0f);

        Vector3 test = prefab.transform.rotation.eulerAngles;
        print(test);
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
        //obj.transform.position = testPoint;
        //obj.transform.Rotate(testRota);
    }
}
