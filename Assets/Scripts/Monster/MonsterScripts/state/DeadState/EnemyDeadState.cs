using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeadState : EnemyState
{
    public EnemyDeadState(MonsterController monsterState) : base(monsterState){}

    static readonly int IsDead = Animator.StringToHash("IsDead");

    public override void Enter()
    {
        monsterController.animator.SetTrigger(IsDead);

        DeadAnimationStart();
    }

    public override void Exit()
    {
        
    }

    public override void Update()
    {
    }

    void DeadAnimationStart()
    {
        monsterController.StartCoroutine(DeadAnimation());
    }

    IEnumerator DeadAnimation()
    {
        float time = 0;

        yield return new WaitForSeconds(5.0f);


        Collider collider = monsterController.GetComponentInChildren<CapsuleCollider>();

        if (collider != null)
        {
            collider.enabled = false;
        }


        while (true)
        {
            time += 0.1f;
            Vector3 downwardMovement = new Vector3(0, -0.1f, 0);
            monsterController.transform.position += downwardMovement;
            yield return new WaitForSeconds(0.1f);

            if (time >= 3.0f)
            {
                Destroy(monsterController.gameObject);
                break;
            }
        }


    }
}
