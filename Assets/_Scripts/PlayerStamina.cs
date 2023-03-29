using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    [SerializeField] private float maxStamina = 100f;
    [SerializeField] private float jumpStamina = 50f;
    [SerializeField] private float staminaRegenerationSpeed = 15f;

    private float currentStamina = 100f;

    private void Update()
    {
        currentStamina = currentStamina < maxStamina ? currentStamina + staminaRegenerationSpeed * Time.deltaTime : currentStamina;
    }

    public void JumpStaminaUse()
    {
        currentStamina -= jumpStamina;
    }

    public bool CanJump()
    {
        return currentStamina - jumpStamina >= 0f;
    }

}
