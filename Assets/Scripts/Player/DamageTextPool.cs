using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageTextPool : MonoBehaviour
{
    public GameObject dmgText;
    Queue<GameObject> pool = new Queue<GameObject>();

    void Awake()
    {
        for(int i =0; i < 8; i++)
        {
            GameObject text = Instantiate(dmgText, transform);
            text.SetActive(false);
            pool.Enqueue(text);
        }
    }

    public GameObject GetObj(Transform target, float dmg)
    {
        if(pool.Count == 0)
        {
            GameObject text = Instantiate(dmgText, transform);
            pool.Enqueue(text);
        }

        GameObject obj = pool.Dequeue();
        obj.GetComponent<TextMeshPro>().text = dmg.ToString();
        obj.SetActive(true);
        obj.transform.SetParent(target);
        obj.transform.position = Vector3.zero;
        return obj;
    }

    public void ReturnObj(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.SetParent(transform);
        pool.Enqueue(obj);
    }
}