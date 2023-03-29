using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnCollision : MonoBehaviour
{
    [SerializeField] private int damage = 10;

    private Collider2D coll;
    private void Start()
    {
        coll = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(LayerMask.NameToLayer("Player") ==  collision.gameObject.layer)
        {
            GameManager.Player.OnHit(damage, coll);
        }
    }
}
