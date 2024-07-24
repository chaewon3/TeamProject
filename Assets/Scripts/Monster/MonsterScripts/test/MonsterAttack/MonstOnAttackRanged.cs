using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonstOnAttackRanged : MonoBehaviour
{
    public GameObject spherePrefab;

    List<GameObject> spherePrefabList = new List<GameObject>();

    MonsterController monsterController;

    private void Start()
    {
        monsterController = GetComponentInParent<MonsterController>();

        for (int i = 0; i < 2; i++)
        {
            addListSphere();
        }

       
    }


    public void RangedAttack()
    {
        foreach (GameObject sphere in spherePrefabList)
        {
            if (!sphere.activeSelf)
            {
                print("Shoot");
                Shoot(sphere);

                return;
            }
        }

        addListSphere();
        Shoot(spherePrefabList[spherePrefabList.Count - 1]);

    }

    void addListSphere()
    {
        GameObject spherePrefabInstance = Instantiate(spherePrefab);
        spherePrefabInstance.GetComponent<MonsterRangedAttackCollider>().setDamage(monsterController.monsterInfo._attackDamage);
        spherePrefabList.Add(spherePrefabInstance);
    }

    void Shoot(GameObject sphere)
    {
        Vector3 forwardDirection = monsterController.transform.forward;
        Vector3 positionOffset = new Vector3(0, 1.5f, 2);

        Vector3 newPosition = monsterController.transform.position + forwardDirection * positionOffset.z + Vector3.up * positionOffset.y;

        sphere.transform.position = newPosition;

        sphere.transform.rotation = Quaternion.LookRotation(forwardDirection);

        sphere.SetActive(true);
    }

}
