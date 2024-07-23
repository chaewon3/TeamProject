using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPool : MonoBehaviour
{
    #region 전역 변수
    public GameObject prefab;
    public Transform spawnPoint;
    public GameObject spawnObj;
    
    Queue<GameObject> pool = new Queue<GameObject>();
    //int count;
    int poolSize = 12;
    #endregion

    void Awake()
    {
        for(int i =0 ; i < poolSize; i++)
        {
            GameObject arrowObj = Instantiate(prefab, spawnObj.transform);
            arrowObj.SetActive(false);
            pool.Enqueue(arrowObj);
        }
    }

    public GameObject GetArrow()
    {
        if(pool.Count == 0)
        {
            GameObject arrow = Instantiate(prefab, transform);
            pool.Enqueue(arrow);
        }

        GameObject obj = pool.Dequeue();
        obj.transform.position = spawnPoint.position;
        obj.transform.rotation = spawnPoint.rotation;
        obj.SetActive(true);
        obj.transform.SetParent(null);
        return obj;
        //
    }

    public void ReturnArrow(GameObject obj)
    {
        print("발동");
        obj.SetActive(false);
        obj.transform.SetParent(spawnObj.transform);
        pool.Enqueue(obj);
    }
}


//if (count != pool.Count)
//{
//    GameObject arrow = pool[count];
//    arrow.transform.position = spawnPoint.position;
//    arrow.transform.rotation = spawnPoint.rotation;
//    arrow.SetActive(true);
//    arrow.transform.SetParent(null);
//    count++;
//    return arrow;
//}
//else
//{
//    count = 0;
//    GameObject arrow = pool[count];
//    arrow.transform.position = spawnPoint.position;
//    arrow.transform.rotation = spawnPoint.rotation;
//    arrow.SetActive(true);
//    arrow.transform.SetParent(null);
//    count++;
//    return arrow;
//}