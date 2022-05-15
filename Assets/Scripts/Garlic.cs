using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garlic : MonoBehaviour
{
    public float attackSpeed = 10f;
    public float maxDamageCooldown;
    public float damageCooldown;
    public float strength = 5;
    public float range = 1;
    private CircleCollider2D circleCollider2D;
    private SpriteRenderer spriteRenderer;
    

    private void Start()
    {
        maxDamageCooldown = 1 / attackSpeed;

        SetRadius();


    }

    private void SetRadius()
    {
        circleCollider2D = GetComponent<CircleCollider2D>();
        circleCollider2D.radius = range;

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.transform.localScale = new Vector3(range * 2, range * 2, range * 2);


    }

    private void Update()
    {
        damageCooldown -= Time.deltaTime;
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            if (damageCooldown <= 0)
            {
                other.GetComponent<EnemyController>().TakeDamage(strength);
                damageCooldown = maxDamageCooldown;
            }
        }
    }
}
