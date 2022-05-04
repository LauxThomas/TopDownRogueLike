using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    float maxPlayerHealth = 100;
    float playerHealth = 100;
    

    internal void TakeDamage(float damage)
    {
        playerHealth -= damage;
        if (playerHealth <= 0)
        {
            playerHealth = 0;
            Debug.Log("Player is dead");
            Destroy(gameObject);
        }
    }
}
