using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private int pointsGathered = 0;

    [SerializeField] private TMPro.TMP_Text coinsText;
    [SerializeField] private String preCoinsText = "Points: ";

    public void AddPoints(int points)
    {
        pointsGathered += points;
        coinsText.text = preCoinsText + pointsGathered.ToString();
    }
    public void RemovePoints(int points)
    {
        pointsGathered -= points;
        if (pointsGathered < 0)
            pointsGathered = 0;

        coinsText.text = preCoinsText + pointsGathered.ToString();
    }
}
