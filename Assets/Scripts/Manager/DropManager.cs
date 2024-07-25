using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropManager : MonoBehaviour
{
    public static DropManager instance;

    private void Awake()
    {
        instance = this;
    }

    List<ItemDataSO> dropitemSOList = new List<ItemDataSO>();

    private void Start()
    {
        dropitemSOList = DataManager.Instance.itemSOList;
    }

    public void RandomItemDrop(Transform monsterDeadPoint)
    {
        int randValue = Random.Range(0,dropitemSOList.Count);

        if (dropitemSOList[randValue].ModelPrefab == null)
        {
            print(dropitemSOList.Count);
        }
        else
        {
            print(dropitemSOList[randValue].name);
        }

        GameObject dropItem = Instantiate(dropitemSOList[randValue].ModelPrefab, monsterDeadPoint.position + Vector3.up, dropitemSOList[randValue].ModelPrefab.transform.rotation);

    }
}
