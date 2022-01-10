using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : ADamageableObject
{
    [SerializeField]
    private GameObject bombPrefab;

    public void PlaceBomb()
    {
        Instantiate(bombPrefab, gameObject.transform.position, Quaternion.identity);
    }

    protected override void Die()
    {
        Destroy(gameObject);
        //anim, sound
    }
}
