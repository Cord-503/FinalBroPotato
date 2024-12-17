using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Chase-Direct Chase", menuName = "Enemy Logic/Chase Logic/Direct Chase")]

public class EnemyChase : EnemyChaseSOBase
{
    [SerializeField] private float _movementSpeed = 1.75f;

    private SpriteRenderer sr;

    public override void DoEnterlogic()
    {
        base.DoEnterlogic();

        sr = enemy.GetComponentInChildren<SpriteRenderer>();
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();

        Vector2 moveDirection = (playerTransform.position - enemy.transform.position).normalized;

        enemy.MoveEnemy(moveDirection * _movementSpeed);

        FlipSpriteBasedOnPlayerPosition();
    }

    private void FlipSpriteBasedOnPlayerPosition()
    {
        if (sr == null || playerTransform == null) return;

        if (playerTransform.position.x < enemy.transform.position.x)
        {
            sr.flipX = false;
        }
        else
        {
            sr.flipX = true;
        }
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }

    public override void Initialize(GameObject gameObject, Enemy enemy)
    {
        base.Initialize(gameObject, enemy);
    }

    public override void ResetValues()
    {
        base.ResetValues();
    }
}
