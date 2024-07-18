using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveState : EnemyState
{
    public EnemyMoveState(MonsterController character) : base(character) { }

    Coroutine searching;


    public override void Enter()
    {
        if (searching == null)
        {
            searching = monsterController.StartCoroutine(SearchCharacter());
        }

        monsterController.animator.applyRootMotion = false;

        monsterController._isMove = true;

        monsterController.animator.SetBool("IsMove", true);

        print("enter the Move State");
    }

    public override void Update()
    {

        if (monsterController._isMove)
        {
            if (monsterController._characterGotIntoArea && monsterController.PlayerObject != null && monsterController._characterTransfrom != null)
            {
                // 캐릭터에게로

                if (monsterController.PlayerObject == null)
                {
                    print("wwww");
                }

                if (monsterController._characterTransfrom == null)
                {
                    print("sss");
                }
                MonsterMove(monsterController._characterTransfrom.position, monsterController.monsterInfo._attackDetectRange, true);
            }
            else
            {
                // 원래 자리로
                MonsterMove(monsterController._monsterOriginPosition, monsterController.monsterInfo._returnStopRange, false);
            }
        }
        else
        {
            monsterController.TransitionToState(monsterController.attackState);
        }

    }

    public override void Exit()
    {
        if (searching != null)
        {
            monsterController.StopCoroutine(searching);

            searching = null;
        }

        monsterController.animator.applyRootMotion = true;


        monsterController._isMove = false;

        monsterController.animator.SetBool("IsMove", false);
    }

    private IEnumerator SearchCharacter()
    {
        while (true)
        {
            if (monsterController.PlayerObject != null)
            {
                LoadCharacterTransform();
            }

            print("searching");

            yield return new WaitForSeconds(1.0f);
        }
    }

    public void LoadCharacterTransform()
    {
        monsterController._characterTransfrom = monsterController.PlayerObject.transform;
    }

    void MonsterMove(Vector3 targetPos, float stopRange, bool IsTracing)
    {
        Vector3 currentPosition = monsterController.transform.position;
        Vector3 targetPosition = targetPos;

        Vector3 direction = (targetPosition - currentPosition).normalized;
        direction.y = 0;


        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            monsterController.transform.rotation = Quaternion.Slerp(monsterController.transform.rotation, targetRotation, 3.0f * Time.deltaTime);
        }



        monsterController.transform.position = Vector3.MoveTowards(currentPosition, new Vector3(targetPosition.x, currentPosition.y, targetPosition.z), monsterController.monsterInfo._moveSpeed * Time.deltaTime);

        if ((IsTracing) && Vector3.SqrMagnitude(currentPosition - targetPosition) <= (stopRange * stopRange))
        {
            // 공격 스테이트로
            print("attack");
            monsterController.TransitionToState(monsterController.attackState);
        }

        if ((!IsTracing) && (Vector3.SqrMagnitude(currentPosition - targetPosition) <= (stopRange * stopRange)))
        {
            monsterController.TransitionToState(monsterController.idleState);
            
        }
    }
}
