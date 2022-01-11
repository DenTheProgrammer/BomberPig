using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class Player : ADamageableObject
{
    [SerializeField]
    private GameObject bombPrefab;
    private PhotonView view;
    private TextMeshProUGUI healthText;

    private void Start()
    {
        view = GetComponent<PhotonView>();
        healthText = GameObject.Find("healthText").GetComponent<TextMeshProUGUI>();
        BombButton.onBombButtonPress += PlaceBomb;
        UpdateHealthText();
    }

    private void OnDestroy()
    {
        BombButton.onBombButtonPress -= PlaceBomb;
    }

    public void PlaceBomb()
    {
        if (view.IsMine)
            PhotonNetwork.Instantiate(bombPrefab.name, gameObject.transform.position + new Vector3(0f, -0.1f), Quaternion.identity);
        //Instantiate(bombPrefab, gameObject.transform.position + new Vector3(0f, -0.1f), Quaternion.identity);
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        UpdateHealthText();
    }

    private void UpdateHealthText()
    {
        healthText.text = this.health.ToString();
    }

    protected override void Die()
    {
        base.Die();
        //anim, sound
    }


}
