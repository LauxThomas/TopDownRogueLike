using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garlic : MonoBehaviour
{
    public float attackSpeed = 10f;
    public float maxDamageCooldown;
    public float damageCooldown;
    public float strength = 5;

    private void Start()
    {
        maxDamageCooldown = 1 / attackSpeed;
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
