using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState : MonoBehaviour
{
    protected MonsterController monsterState;

    public EnemyState(MonsterController monsterState)
    {
        this.monsterState = monsterState;
    }

    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}
