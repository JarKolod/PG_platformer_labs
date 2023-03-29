using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] Vector2 lastPoint = Vector2.zero;
    [SerializeField] float speed = 0.2f;
    [SerializeField] float targetPosisionError = 0.1f;

    private Vector2 initialPoint;
    private Vector2 targetPos = Vector2.zero;

    private Rigidbody2D rb;

    private void OnEnable()
    {
        initialPoint = transform.position;
        rb = GetComponent<Rigidbody2D>();

        targetPos = lastPoint;
    }

    private void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, targetPos) <= targetPosisionError)
            if(targetPos == initialPoint)
                targetPos = lastPoint;
            else
                targetPos = initialPoint;

        transform.position = Vector2.Lerp(transform.position, targetPos, speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.SetParent(transform);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
    }
}
