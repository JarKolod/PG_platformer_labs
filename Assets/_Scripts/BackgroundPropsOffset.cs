using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundPropsOffset : MonoBehaviour
{
    [SerializeField] private Transform playerCamera;
    [SerializeField] private float offsetMultiplier = 0f;

    void Update()
    {
        transform.position = new Vector3(playerCamera.position.x * offsetMultiplier, transform.position.y, transform.position.z);
    }
}
