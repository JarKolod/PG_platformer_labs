using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private static float timeLimit = 20f;
    [SerializeField] private Canvas gameOverCanvas;
    [SerializeField] private Canvas HUDCanvas;
    [SerializeField] private TMPro.TMP_Text timeCounter;

    private float roundTimeCounter = 0f;

    public static Player Player { get; private set; } = null;


    private void Start()
    {
        Player = GameObject.Find("Player").GetComponent<Player>();
    }

    private void Update()
    {
        roundTimeCounter += Time.deltaTime;
        timeCounter.text = string.Format("{0}:{1}", 
            TimeSpan.FromSeconds(timeLimit - roundTimeCounter).Minutes, 
            TimeSpan.FromSeconds(timeLimit - roundTimeCounter).Seconds);

        if (timeLimit - roundTimeCounter <= 10f)
        {
            timeCounter.color = Color.red;
        }

        if (roundTimeCounter >= timeLimit)
        {
            roundTimeCounter = 0f;
            Time.timeScale = 0f;
            GameOverTime();
        }
    }

    private void GameOverTime()
    {
        HUDCanvas.gameObject.SetActive(false);
        gameOverCanvas.gameObject.SetActive(true);
    }

}

