
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitState : EnemyState
{
    public EnemyHitState(MonsterController monsterState) : base(monsterState){}
    static readonly int GotHit = Animator.StringToHash("GotHit");


    public override void Enter()
    {
        
        monsterController.StartCoroutine(HitMotion());
    }

    public override void Exit()
    {
        
    }

    public override void Update()
    {
        
    }

    IEnumerator HitMotion()
    {
        monsterController.animator.SetTrigger(GotHit);
        yield return new WaitForSeconds(1.5f);
        monsterController.TransitionToState(monsterController.moveState);
    }

    void TurningWhenHit()
    {
        Vector3 currentPosition = monsterController.transform.position;
        Vector3 targetPosition = monsterController._characterTransfrom.position;

        Vector3 direction = (targetPosition - currentPosition).normalized;
        direction.y = 0;


        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            monsterController.transform.rotation = targetRotation;
        }
    }
}
