using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterOnAttackRanged : MonoBehaviour
{
    public GameObject spherePrefab;

    List<GameObject> spherePrefabList = new List<GameObject>();

    public GameObject bubblePrefab;

    List<GameObject> bubblePrefabList = new List<GameObject>();

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


        AcitvatingShot(bubblePrefabList, 1);

        AcitvatingShot(spherePrefabList, 0);




    }

    void AcitvatingShot(List<GameObject> spherePrefabList, int index)
    {
        foreach (GameObject sphere in spherePrefabList)
        {
            if (!sphere.activeSelf)
            {
                print("Shoot");
                Shoot(sphere, index);

                return;
            }
        }

        addListSphere();
        Shoot(spherePrefabList[spherePrefabList.Count - 1], index);
    }


    void addListSphere()
    {
        GameObject spherePrefabInstance = Instantiate(spherePrefab);
        spherePrefabInstance.GetComponent<MonsterRangedAttackCollider>().setDamage(monsterController.monsterInfo._attackDamage);
        spherePrefabList.Add(spherePrefabInstance);



        GameObject bubblePrefabInstance = Instantiate(bubblePrefab);
        bubblePrefabList.Add(bubblePrefabInstance);
    }

    void Shoot(GameObject sphere, int index)
    {
        Vector3 forwardDirection = monsterController.transform.forward;
        Vector3 positionOffset = new Vector3(0, 1.5f, 2);

        Vector3 newPosition = monsterController.transform.position + forwardDirection * positionOffset.z + Vector3.up * positionOffset.y;


        sphere.transform.position = newPosition;

        sphere.transform.rotation = Quaternion.LookRotation(forwardDirection);

        sphere.SetActive(true);

        if (index == 1)
        {
            StartCoroutine(deactivate(sphere));
        }

    }

    IEnumerator deactivate(GameObject bubble)
    {
        yield return new WaitForSeconds(4.0f);
        bubble.SetActive(false);
    }




}
