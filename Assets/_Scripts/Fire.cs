using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    private Collider2D coll;
    private bool canGetHit = true;

    [SerializeField] private int fireDamage = 5;
    [SerializeField] private float timeBetweenHits = 1f;

    private void Start()
    {
        coll = GetComponent<Collider2D>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.transform.name == "Player" && canGetHit)
        {
            GameManager.Player.OnHit(fireDamage, coll);
            canGetHit = false;
            StartCoroutine(FireHit());
        }
    }

    IEnumerator FireHit()
    {
        yield return new WaitForSeconds(timeBetweenHits);
        canGetHit = true;
    }
}
