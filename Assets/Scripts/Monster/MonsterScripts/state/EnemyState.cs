using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState : MonoBehaviour
{
    protected MonsterController monsterController;

    public EnemyState(MonsterController monsterState)
    {
        this.monsterController = monsterState;
    }

    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}
