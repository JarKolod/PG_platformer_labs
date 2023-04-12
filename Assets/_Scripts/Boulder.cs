using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boulder : MonoBehaviour
{
    private ParticleSystem ps;
    private SpriteRenderer sprite;
    private Rigidbody2D rb;
    private CircleCollider2D coll;


    private void Start()
    {
        ps = GetComponentInChildren<ParticleSystem>();
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<CircleCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.name == "Player")
        {
            GameManager.Player.OnHit(10, coll);
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Terrain") || collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            StartCoroutine(DestroyAfterEffects());
        }
    }

    IEnumerator DestroyAfterEffects()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        coll.isTrigger = true;
        ps.Play();
        sprite.enabled = false;
        yield return new WaitForSeconds(ps.main.duration);
        Destroy(this.gameObject);
    }
}
