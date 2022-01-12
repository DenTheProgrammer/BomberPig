using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : ADamageableObject
{
    protected override void Die()
    {
        base.Die();
        //anim, sound
    }
}
