using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class health : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public virtual void TakeDamage(int damage)
    {
        // make visual shiet happen
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            GameObject da = ObjectPooler.Instance.Spawn("death_anim", transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public virtual void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
    }
}
