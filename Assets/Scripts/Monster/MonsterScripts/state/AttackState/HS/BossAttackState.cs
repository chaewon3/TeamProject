using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackState : EnemyAttackState
{
    GameObject mummyPrefab;
    List<GameObject> listOfMummy = new List<GameObject>();

    GameObject rockPrefab;
    List<GameObject> listOfRock = new List<GameObject>();

    public BossAttackState(MonsterController character) : base(character) 
    {
        mummyPrefab = Resources.Load<GameObject>("Mummy In Standing Coffin");

        for (int i = 0; i < 10; i++)
        {
            addListInstance(listOfMummy, 0);
        }

        rockPrefab = Resources.Load<GameObject>("DropPillar");

        for (int i = 0; i < 2; i++)
        {
            addListInstance(listOfRock, 1);
        }
    }

    static readonly int PatternAttack = Animator.StringToHash("PatternAttack");
    static readonly int PatternSpellOne = Animator.StringToHash("PatternSpellOne");
    static readonly int PatternSpellTwoCharging = Animator.StringToHash("PatternSpellTwoCharging");
    static readonly int PatternSpellTwoShot = Animator.StringToHash("PatternSpellTwoShot");

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    protected override void PatternCooltime(System.Enum @enum)
    {
        BOSS_MONSTER_ATTACK_BEHAVIOUR bossPattern = (BOSS_MONSTER_ATTACK_BEHAVIOUR)@enum;

        switch (bossPattern)
        {
            case BOSS_MONSTER_ATTACK_BEHAVIOUR.BOSS_MONSTER_ATTACK:
                print(bossPattern);
                monsterController.StartCoroutine(BossPatternAttack(monsterController.monsterInfo.attackDurationTime));
                break;
            case BOSS_MONSTER_ATTACK_BEHAVIOUR.BOSS_MONSTER_SKILL_1:
                print(bossPattern);
                monsterController.StartCoroutine(BossPatternSpellOne());
                monsterController.StartCoroutine(BossPatternSpellOneCooltime(BOSS_MONSTER_ATTACK_BEHAVIOUR.BOSS_MONSTER_SKILL_1, 15));
                break;
            case BOSS_MONSTER_ATTACK_BEHAVIOUR.BOSS_MONSTER_SKILL_2:
                print(bossPattern);
                monsterController.StartCoroutine(BossPatternSpellTwo());
                monsterController.StartCoroutine(BossPatternSpellOneCooltime(BOSS_MONSTER_ATTACK_BEHAVIOUR.BOSS_MONSTER_SKILL_2, 20));
                break;
        }
    }

    IEnumerator BossPatternAttack(float time)
    {
        monsterController.animator.SetTrigger(PatternAttack);
        yield return new WaitForSeconds(time);
        if (!monsterController._isHit && !monsterController._isDead)
        {
            monsterController.TransitionToState(monsterController.moveState);
        }
    }

    IEnumerator BossPatternSpellOne()
    {
        monsterController.animator.SetTrigger(PatternSpellOne);

        yield return new WaitForSeconds(2f);

        foreach (var item in listOfRock)
        {
            item.transform.position = monsterController._characterTransfrom.position + Vector3.up * 7;
            item.SetActive(true);
            yield return new WaitForSeconds(1.0f);
        }

        if (!monsterController._isHit && !monsterController._isDead)
        {
            monsterController.TransitionToState(monsterController.moveState);
        }
    }

    IEnumerator BossPatternSpellTwo()
    {
        monsterController.animator.SetTrigger(PatternSpellTwoCharging);

        yield return new WaitForSeconds(3f);

        SpawnPrefab(listOfMummy, 0);

        monsterController.animator.SetTrigger(PatternSpellTwoShot);

        yield return new WaitForSeconds(1.1f);
        if (!monsterController._isHit && !monsterController._isDead)
        {
            monsterController.TransitionToState(monsterController.moveState);
        }
    }

    IEnumerator BossPatternSpellOneCooltime(System.Enum @enum, float cooltime)
    {
        monsterController.monsterInfo._monsterBehaviourPool[@enum] = false;
        yield return new WaitForSeconds(cooltime);
        monsterController.monsterInfo._monsterBehaviourPool[@enum] = true;
    }

    void addListInstance(List<GameObject> listOfPrefab, int index)
    {
        GameObject prefabInstance;

        if (index == 0)
        {
            prefabInstance = Instantiate(mummyPrefab, Vector3.zero, Quaternion.identity);
        }
        else
        {
            prefabInstance = Instantiate(rockPrefab, Vector3.zero, Quaternion.identity);
        }

        listOfPrefab.Add(prefabInstance);
    }

    void SpawnPrefab(List<GameObject> mummyList, int index)
    {
        foreach (GameObject item in mummyList)
        {
            if (!item.activeSelf)
            {
                ActivatingPrefab(item);

                return;
            }
        }

        addListInstance(mummyList, index);
        ActivatingPrefab(mummyList[mummyList.Count - 1]);

    }

    void ActivatingPrefab(GameObject mummy)
    {
        Vector3 forwardDirection = monsterController.transform.forward;
        Vector3 positionOffset = new Vector3(0, 3, 3);

        Vector3 newPosition = monsterController.transform.position + forwardDirection * positionOffset.z + Vector3.up * positionOffset.y;

        mummy.transform.position = newPosition;

        mummy.transform.rotation = Quaternion.LookRotation(forwardDirection);

        mummy.SetActive(true);
    }
}
