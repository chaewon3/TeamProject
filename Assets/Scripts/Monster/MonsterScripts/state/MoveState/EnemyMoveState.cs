using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveState : EnemyState
{
    public EnemyMoveState(MonsterController character) : base(character) { }

    Coroutine searching;
    float angle;

    static readonly int IsMove = Animator.StringToHash("IsMove");

    public override void Enter()
    {
        if (searching == null)
        {
            searching = monsterController.StartCoroutine(SearchCharacter());
        }

        monsterController.gameObject.transform.GetChild(0).transform.localPosition = new Vector3(0, 0, 0);
        monsterController.gameObject.transform.GetChild(0).transform.localEulerAngles = new Vector3(0, 0, 0);


        monsterController.animator.applyRootMotion = false;

        monsterController._isMove = true;

        monsterController.animator.SetBool(IsMove, true);
    }

    public override void Update()
    {
        if (monsterController._isMove)
        {
            //monsterController.PlayerObject != null && monsterController._characterGotIntoArea && ��
            if (monsterController._characterTransfrom != null && (monsterController.monsterInfo._IsAttacked || monsterController._characterGotIntoArea))
            {
                // ĳ���Ϳ��Է�
                //print("ToCharacter");
                MonsterMove(monsterController._characterTransfrom.position, monsterController.monsterInfo._attackDetectRange, true);
            }
            else if((!monsterController.monsterInfo._IsAttacked || !monsterController._characterGotIntoArea))
            {
                // ���� �ڸ���
                //print("ToOrigin");
                MonsterMove(monsterController._monsterOriginPosition, monsterController.monsterInfo._returnStopRange, false);
            }

            if (!monsterController._characterGotIntoArea && monsterController.monsterInfo._IsAttacked && (Vector3.SqrMagnitude(monsterController._monsterOriginPosition - monsterController.transform.position) >= (monsterController.monsterInfo._MaxChasingRange * monsterController.monsterInfo._MaxChasingRange)))
            {
                monsterController.monsterInfo._IsAttacked = false;
                monsterController._characterTransfrom = null;
            }
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

        monsterController.monsterInfo._IsAttacked = false;

        monsterController.animator.SetBool(IsMove, false);
    }

    private IEnumerator SearchCharacter()
    {
        while (true)
        {
            
            if (monsterController.PlayerObject != null)
            {
                LoadCharacterTransform();
            }
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

        if (IsTracing && Vector3.SqrMagnitude(currentPosition - targetPosition) <= (stopRange * stopRange))
        {
            // �ڿ� �־ �Ÿ��ȿ� Ž���Ǳ� ������ ���� �������� 30�� �ȿ� ������ ����
            angle = Vector3.Angle(monsterController.transform.forward, direction);

            if (angle < 30f)
            {
                monsterController.TransitionToState(monsterController.attackState);
            }
        }

        if ((!IsTracing) && (Vector3.SqrMagnitude(currentPosition - targetPosition) <= (stopRange * stopRange)))
        {
            monsterController.TransitionToState(monsterController.idleState);
            
        }
    }
}
