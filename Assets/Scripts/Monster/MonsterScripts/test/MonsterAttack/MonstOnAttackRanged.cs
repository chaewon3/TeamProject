using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonstOnAttackRanged : MonoBehaviour
{
    GameObject spherePrefab;

    List<GameObject> spherePrefabList = new List<GameObject>();

    MonsterController monsterController;

    private void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            addListSphere();
        }

        monsterController = GetComponentInParent<MonsterController>();
    }


    public void RangedAttack()
    {
        foreach (GameObject sphere in spherePrefabList)
        {
            if (!sphere.activeSelf)
            {
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
    }

}
