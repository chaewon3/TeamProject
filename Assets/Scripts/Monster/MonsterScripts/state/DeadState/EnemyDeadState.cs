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
        monsterController._isDead = true;

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

        Collider collider = monsterController.GetComponentInChildren<CapsuleCollider>();
        Rigidbody rigidbody = monsterController.GetComponent<Rigidbody>();

        if (collider != null)
        {
            collider.enabled = false;
        }
        if (rigidbody != null)
        {
            rigidbody.isKinematic = true;
        }
        yield return new WaitForSeconds(5.0f);


        while (true)
        {
            time += 0.01f;
            Vector3 downwardMovement = new Vector3(0, -0.01f, 0);
            monsterController.transform.position += downwardMovement;
            yield return new WaitForSeconds(0.01f);

            if (time >= 3.0f)
            {
                //Destroy(monsterController.gameObject);
                monsterController.gameObject.SetActive(false);
                break;
            }
        }


    }
}
