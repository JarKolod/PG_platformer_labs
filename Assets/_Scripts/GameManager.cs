using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject playerGameObj;

    public static Player Player { get; private set; } = null;

    private void Start()
    {
        Player = GameObject.Find("Player").GetComponent<Player>();
    }

}
