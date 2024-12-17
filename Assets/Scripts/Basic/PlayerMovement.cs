using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D body;
    SpriteRenderer sr;

    float horizontal;
    float vertical;

    [SerializeField] float moveLimiter = 0.7f;
    [SerializeField] float speed = 20.0f;

    [Header("Attack Object")]
    public GameObject attackObject;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();

        if (attackObject != null)
        {
            attackObject.transform.SetParent(transform);
            attackObject.SetActive(false);
        }
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            attackObject.SetActive(true);
            attackObject.GetComponent<AttackFollowMouse>()?.TriggerAttack();
        }

    }

    private void FixedUpdate()
    {
        if (horizontal != 0 && vertical != 0)
        {
            horizontal *= moveLimiter;
            vertical *= moveLimiter;
        }

        body.velocity = new Vector2(horizontal * speed, vertical * speed);

        FlipSpriteBasedOnDirection();
    }

    private void FlipSpriteBasedOnDirection()
    {
        if (sr == null) return;

        if (horizontal < 0)
        {
            sr.flipX = true;
        }
        else if (horizontal > 0)
        {
            sr.flipX = false;
        }
    }
}
