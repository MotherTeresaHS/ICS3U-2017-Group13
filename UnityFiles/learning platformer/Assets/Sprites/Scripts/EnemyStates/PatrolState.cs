using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IEnemyState
{
    private Enemy enemy;
    private float patrolTimer;
    private float partrolDuration;

    public void Enter(Enemy enemy)
    {
        partrolDuration = UnityEngine.Random.Range(1, 10); 
        this.enemy = enemy;
    }

    public void Execute()
    {
        Patrol();

        enemy.Move();

        if (enemy.Target != null && enemy.InThrowRange)
        {
            enemy.ChangeState(new RangedState());
        }
    }

    public void Exit()
    {

    }

    public void OnTriggerEnter(Collider2D other)
    {
          if (other.tag == "Knife")
        {
            enemy.Target = player.Instance.gameObject;
        }
    }
    private void Patrol()
    {
        patrolTimer += Time.deltaTime;

        if (patrolTimer >= partrolDuration)
        {
            enemy.ChangeState(new IdleState());
        }
    }

}
