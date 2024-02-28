using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : health
{
    public override void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Debug.Log("died");
            Destroy(gameObject);
        }
    }
}
