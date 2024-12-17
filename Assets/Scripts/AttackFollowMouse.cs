using UnityEngine;
using UnityEngine.InputSystem;

public class AttackFollowMouse : MonoBehaviour
{
    private Camera _cam;
    private Transform playerTransform;

    [SerializeField] private float attackDuration = 0.5f;
    private float attackTimer = 0f;
    private bool isAttacking = false;

    private float angle;

    void Start()
    {
        _cam = Camera.main;
        playerTransform = transform.parent;
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (isAttacking)
        {
            RotateAroundPlayer();
            attackTimer += Time.deltaTime;

            if (attackTimer >= attackDuration)
            {
                EndAttack();
            }
        }
    }

    public void TriggerAttack()
    {
        if (!isAttacking)
        {
            StartAttack();
        }
    }

    private void StartAttack()
    {
        isAttacking = true;
        gameObject.SetActive(true);
        attackTimer = 0f;
    }

    private void EndAttack()
    {
        isAttacking = false;
        gameObject.SetActive(false);
        attackTimer = 0f;
    }

    private void RotateAroundPlayer()
    {
        float rotationSpeed = 360f / attackDuration * Time.deltaTime;

        angle += rotationSpeed;
        float radius = Vector3.Distance(playerTransform.position, transform.position);

        Vector3 offset = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0) * radius;

        transform.position = playerTransform.position + offset;

        transform.rotation = Quaternion.identity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
        }
    }
}
