using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack", menuName = "Enemy Logic/Attack Logic/Attack")]
public class EnemyAttack : EnemyAttackSOBase
{
    [Header("General Attack Settings")]
    [SerializeField] private bool ifRanged = true;
    [SerializeField] private float _timerTillExit = 1f;

    [Header("Ranged Settings")]
    [SerializeField] private Rigidbody2D BulletPrefab;
    [SerializeField] private float _timeBetweenShots = 2f;
    [SerializeField] private float _bulletSpeed = 10f;

    [Header("Melee Settings")]
    [SerializeField] private float meleeAttackSpeed = 5f;
    [SerializeField] private int meleeDamage = 20;
    [SerializeField] private float meleeAttackCooldown = 1.5f;

    private float _timer;
    private float _exitTimer;
    private float _meleeAttackTimer;

    private SpriteRenderer sr;

    public override void DoEnterlogic()
    {
        base.DoEnterlogic();
        sr = enemy.GetComponentInChildren<SpriteRenderer>();
        _meleeAttackTimer = 0f;
    }
    #region Logics
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
    #endregion

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();

        FlipSpriteBasedOnPlayerPosition();

        if (ifRanged)
        {
            HandleRangedAttack();
        }
        else
        {
            HandleMeleeAttack();
        }
    }

    private void HandleRangedAttack()
    {
        enemy.MoveEnemy(Vector2.zero);

        if (_timer > _timeBetweenShots)
        {
            _timer = 0f;

            Vector2 dir = (playerTransform.position - enemy.transform.position).normalized;

            Rigidbody2D bullet = GameObject.Instantiate(BulletPrefab, enemy.transform.position, Quaternion.identity);
            bullet.velocity = dir * _bulletSpeed;
        }

        if (!enemy.IsWithinstrikingDistance)
        {
            _exitTimer += Time.deltaTime;

            if (_exitTimer > _timerTillExit)
            {
                enemy.stateMachine.ChangeState(enemy.ChaseState);
            }
        }
        else
        {
            _exitTimer = 0f;
        }

        _timer += Time.deltaTime;
    }

    private void HandleMeleeAttack()
    {
        Vector2 direction = (playerTransform.position - enemy.transform.position).normalized;
        enemy.MoveEnemy(direction * meleeAttackSpeed);

        _meleeAttackTimer += Time.deltaTime;

        if (enemy.IsWithinstrikingDistance && _meleeAttackTimer >= meleeAttackCooldown)
        {
            DealMeleeDamage();
            _meleeAttackTimer = 0f;
        }
    }

    private void DealMeleeDamage()
    {
        if (playerTransform != null)
        {
            PlayerHealthManager playerHealth = playerTransform.GetComponent<PlayerHealthManager>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(meleeDamage);
                Debug.Log("Melee Attack: Player takes " + meleeDamage + " damage.");
            }
        }
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
}
