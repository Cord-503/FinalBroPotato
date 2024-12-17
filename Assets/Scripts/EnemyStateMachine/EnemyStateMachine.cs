using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine 
{
    public EnemyState curEnemyState {  get; set; }
   
    public void Initialize(EnemyState enemyState)
    {
        curEnemyState = enemyState;
        curEnemyState.EnterState();
    }
     public void ChangeState(EnemyState newState)
    {
        curEnemyState.ExitState();
        curEnemyState = newState;
        curEnemyState.EnterState();
    }
}
