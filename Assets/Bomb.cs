using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField]
    private int damage = 1;
    [SerializeField]
    private float explosionRadius = 2f;
    [SerializeField]
    private float timer = 1.5f;
    private float startingTime;


    private void Start()
    {
        startingTime = Time.time;
    }

    private void Update()
    {
        CheckTimer();
    }

    private void CheckTimer()
    {
        if (Time.time - startingTime >= timer)
            Explode();
    }


    private void Explode()
    {
        //anim,sound
        Collider2D[] collidersInRadius;
        collidersInRadius = Physics2D.OverlapCircleAll(gameObject.transform.position, explosionRadius);
        foreach (var collider in collidersInRadius)
        {
            ADamageableObject damageableObject = collider.gameObject.GetComponent<ADamageableObject>();
            if (damageableObject)
            {
                damageableObject.TakeDamage(damage);
            }
        }
        Destroy(gameObject);
    }
}
