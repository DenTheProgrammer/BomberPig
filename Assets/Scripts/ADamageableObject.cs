using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ADamageableObject : MonoBehaviour
{
    [SerializeField]
    protected int health = 1;

    public virtual void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
            Die();
    }

    protected virtual void Die() {
        Destroy(gameObject);
    }
}
