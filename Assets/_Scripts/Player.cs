using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private Animator anim;
    private SpriteRenderer spriteRend;

    private PlayerStamina playerStamina;
    private PlayerInventory playerInventory;

    private float velocityX = 0f;
    private bool isJumping = false;
    private float currentVelX;

    private bool isAttacking = false;

    private int currentHp;

    public Action<int> OnCoinCollect;
    public Action<int,Collider2D> OnHit;
    public Action OnDeath;

    [SerializeField] private int maxHP = 100;
    [SerializeField] private Slider healthBarSlider;
    [Space]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 250f;
    [SerializeField] private float smoothingTimeWalking = 0.1f;
    [SerializeField] LayerMask terrain;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();

        playerStamina = GetComponent<PlayerStamina>();
        playerInventory = GetComponent<PlayerInventory>();

        currentHp = maxHP;

        OnCoinCollect += AddPoints;
        OnHit += OnPlayerHit;
        OnDeath += HandlePlayersDeath;

        healthBarSlider.minValue = 0f;
        healthBarSlider.maxValue = maxHP;
        healthBarSlider.value = currentHp;
    }

    private void Update()
    {

        isAttacking = Input.GetKey(KeyCode.J);
        HandleVelocityInput();
        HandleJumpInput();
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(
            Mathf.SmoothDamp(rb.velocity.x, velocityX * speed, ref currentVelX, smoothingTimeWalking),
            rb.velocity.y);

        if (isJumping && playerStamina.CanJump())
        {
            playerStamina.JumpStaminaUse();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        HandleAnimations();

        velocityX = 0f;
        isJumping = false;
    }

    private void HandleJumpInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
        }
    }

    private void HandleVelocityInput()
    {
        if (Input.GetKey(KeyCode.D))
        {
            velocityX = 1f;
            spriteRend.flipX = false;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            velocityX = -1f;
            spriteRend.flipX = true;
        }
        else
        {
            velocityX = 0f;
        }
    }

    private void HandleAnimations()
    {
        JumpingAnimationTransition();
        RunningAnimationTransition();
        AttackAnimationTranssition();
    }

    private void AttackAnimationTranssition()
    {
        anim.SetBool("IsAttacking",isAttacking);
    }

    private void RunningAnimationTransition()
    {
        anim.SetFloat("VelocityX", Mathf.Abs(rb.velocity.x));
    }

    private void JumpingAnimationTransition()
    {
        if (IsGrounded())
        {
            anim.SetBool("IsJumping", false);
        }
        else
        {
            if(!isAttacking)
                anim.SetBool("IsJumping", true);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(transform.position, new Vector2(coll.size.x, coll.size.y / 3f), 0f, Vector2.down, coll.size.y/2f, terrain) && Mathf.Abs(rb.velocity.y) <= 0.1f;
    }

    private void AddPoints(int points)
    {
        playerInventory.AddPoints(points);
    }

    private void RemovePoints(int points)
    {
        playerInventory.RemovePoints(points);
    }

    private void OnPlayerHit(int damage, Collider2D other)
    {
        Vector2 throwBack = new Vector2(transform.position.x - other.transform.position.x, transform.position.y - other.transform.position.y).normalized;

        rb.velocity = throwBack * 10f;
        TakeDamge(damage);
        RemovePoints(5);
    }

    private void TakeDamge(int damage)
    {
        print("player hit");
        currentHp -= damage;
        healthBarSlider.value = currentHp;
        if (currentHp <= 0)
        {
            OnDeath();
        }
    }

    private void HandlePlayersDeath()
    {
        print("Player is dead");
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.DrawWireCube(transform.position, coll.size);
        }
    }
}
