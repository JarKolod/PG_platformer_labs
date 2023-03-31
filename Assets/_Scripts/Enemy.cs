using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private Animator anim;
    private Collider2D coll;

    [SerializeField] private Slider hpSlider;
    [Space]
    [SerializeField] private int damage = 20;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float rayDistance = 1f;
    [SerializeField] private float attackDistance = 1f;
    [SerializeField] private float maxHP = 100f;

    private float hp;

    private void Start()
    {
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();

        hp = maxHP;
        hpSlider.minValue = 0f;
        hpSlider.maxValue = maxHP;
        hpSlider.value = hp;
    }

    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, rayDistance, playerLayer);

        if(hit)
        {
            anim.SetTrigger("IsAttacking");
        }
    }

    private void Attack()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, rayDistance, playerLayer);
        if(hit.distance <= attackDistance)
        {
            GameManager.Player.OnHit(damage, coll);
        }
    }

    public void OnHit(int damage)
    {
        hp -= damage;
        hpSlider.value = hp;
        print(hpSlider.value);
        if(hp <= 0)
        {
            OnDeath();
        }
    }

    private void OnDeath()
    {
        GameObject.Destroy(gameObject);
    }
}
