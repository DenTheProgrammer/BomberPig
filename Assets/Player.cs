using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Player : ADamageableObject
{
    [SerializeField]
    private GameObject bombPrefab;

    public void PlaceBomb()
    {
        PhotonNetwork.Instantiate(bombPrefab.name, gameObject.transform.position + new Vector3(0f, -0.1f), Quaternion.identity);
        //Instantiate(bombPrefab, gameObject.transform.position + new Vector3(0f, -0.1f), Quaternion.identity);
    }

    protected override void Die()
    {
        Destroy(gameObject);
        //anim, sound
    }
}
