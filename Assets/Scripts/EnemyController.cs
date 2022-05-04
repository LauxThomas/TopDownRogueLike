using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform player;
    public PlayerStats playerController;
    public float health = 100;
    
    public float movementSpeed = 2f;
    public float strength = 1;

    

    public float attackSpeed = 1f;
    public float maxDamageCooldown;
    public float damageCooldown;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerController = player.GetComponent<PlayerStats>();

        maxDamageCooldown = 1 / attackSpeed;
    }

    private void Update()
    {
        damageCooldown -= Time.deltaTime;
    }


    void FixedUpdate()
    {
        if (player != null)
        {
            Vector3 direction = player.position - transform.position;
            transform.Translate(direction.normalized * movementSpeed * Time.deltaTime, Space.World);
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
       
        if (other.CompareTag("Player"))
        {
            if (damageCooldown <= 0)
            {
                playerController.TakeDamage(strength);
                damageCooldown = maxDamageCooldown;
            }
        }
    }

    internal void TakeDamage(float dmg)
    {
        health -= dmg;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
