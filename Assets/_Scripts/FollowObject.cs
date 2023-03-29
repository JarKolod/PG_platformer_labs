using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    [SerializeField] private GameObject followObject;
    [SerializeField] private float smoothTime = 0.1f;
    [SerializeField] Vector2 cameraOffset = Vector2.zero;
    [Space]
    [SerializeField] Vector2 cameraCenter = Vector2.zero;
    [SerializeField] Vector2 cameraBounds = Vector2.zero;
    [Space]
    [SerializeField] float cameraSpeedZoomMultiplier = 1f;

    private Camera cameraPlayer;
    private Vector2 prevPos;
    private Vector2 currentVel;
    private float initialCameraSize = 5f;

    private void OnEnable()
    {
        cameraPlayer = GetComponent<Camera>();
        initialCameraSize = cameraPlayer.orthographicSize;
        print(initialCameraSize);
    }

    private void FixedUpdate()
    {
        Vector2 nextPos = Vector2.SmoothDamp(transform.position, followObject.transform.position, ref currentVel, smoothTime);

        HandleCameraBounds(ref nextPos);
        HandleCameraSpeedZoom(nextPos);

        transform.position = new Vector3(nextPos.x + cameraOffset.x, nextPos.y + cameraOffset.y, transform.position.z);

        prevPos = nextPos;
    }

    private void HandleCameraBounds(ref Vector2 nextPos)
    {
        if (nextPos.x > cameraBounds.x)
        {
            nextPos.x = cameraBounds.x;
        }
        else if (nextPos.x < -cameraBounds.x)
        {
            nextPos.x = -cameraBounds.x;
        }

        if (nextPos.y > cameraBounds.y)
        {
            nextPos.y = cameraBounds.y;
        }
        else if (nextPos.y < -cameraBounds.y)
        {
            nextPos.y = -cameraBounds.y;
        }
    }

    private void HandleCameraSpeedZoom(Vector2 nextPos)
    {
        float distance = Vector2.Distance(prevPos, nextPos);
        cameraPlayer.orthographicSize = (initialCameraSize + cameraSpeedZoomMultiplier * distance) / 1f;
    }
}
