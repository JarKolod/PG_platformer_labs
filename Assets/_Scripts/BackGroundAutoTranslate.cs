using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundAutoTranslate : MonoBehaviour
{
    [SerializeField] private float autoTranslateSpeed = 0.1f;
    [SerializeField] private SpriteRenderer backGroundSprite;

    private float distanceTranslated = 0f;
    private Vector2 initialPos = Vector2.zero;

    private void Awake()
    {
        initialPos = transform.position;
    }

    void Update()
    {
        distanceTranslated += autoTranslateSpeed * Time.deltaTime;

        if(distanceTranslated >= backGroundSprite.bounds.size.x)
        {
            transform.position -= (Vector3.right* backGroundSprite.bounds.size.x - Vector3.right * 0.1f);
            distanceTranslated = 0f;
        }
        else
        {
            transform.Translate(Vector3.right * autoTranslateSpeed * Time.deltaTime);
        }

    }
}
