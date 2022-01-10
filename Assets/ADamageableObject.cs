using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ADamageableObject : MonoBehaviour
{
    [SerializeField]
    private int health = 1;

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
            Die();
    }

    protected abstract void Die();
}
