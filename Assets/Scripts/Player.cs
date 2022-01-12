using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using System;

public class Player : ADamageableObject
{
    [SerializeField]
    private GameObject bombPrefab;
    [SerializeField]
    private int bombMaxCount = 3;
    private int bombCurrentCount;
    [SerializeField]
    private float bombRechargeTime = 2f;
    private float lastBombPlacedTime;
    private PhotonView view;
    private TextMeshProUGUI healthText;
    private TextMeshProUGUI bombText;

    private void Start()
    {
        bombCurrentCount = bombMaxCount;
        view = GetComponent<PhotonView>();
        healthText = GameObject.Find("healthText").GetComponent<TextMeshProUGUI>();
        bombText = GameObject.Find("bombText").GetComponent<TextMeshProUGUI>();
        BombButton.onBombButtonPress += PlaceBomb;
        UpdateText();
    }

    private void OnDestroy()
    {
        BombButton.onBombButtonPress -= PlaceBomb;
    }

    public void PlaceBomb()
    {
        if (view.IsMine)
        {
            if (bombCurrentCount > 0)
            {
                lastBombPlacedTime = Time.time;
                PhotonNetwork.Instantiate(bombPrefab.name, gameObject.transform.position + new Vector3(0f, -0.1f), Quaternion.identity);
                bombCurrentCount--;
                UpdateText();
            } 
        }
    }

    private void CheckBombRecharge()
    {
        if (bombCurrentCount >= bombMaxCount) return;
        if (Time.time - lastBombPlacedTime >= bombRechargeTime)//need to recharge one
        {
            bombCurrentCount++;
            UpdateText();
            lastBombPlacedTime = Time.time;
        }
    }

    private void Update()
    {
        CheckBombRecharge();
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        UpdateText();
    }

    private void UpdateText()
    {
        if (view.IsMine)
        {
            healthText.text = health.ToString();
            bombText.text = bombCurrentCount.ToString();
        }
    }

    protected override void Die()
    {
        base.Die();
        MultiplayerManager multiplayerManager = FindObjectOfType<MultiplayerManager>();
        if (view.IsMine)
        {
            multiplayerManager.ShowLoseScreen();
        }
        else
        {
            Player[] players = FindObjectsOfType<Player>();
            int playersAliveCount = 0;
            foreach (var player in players)
            {
                if (player.health > 0)
                    playersAliveCount++;
            }
            if (playersAliveCount == 1)
            {
                multiplayerManager.ShowWinScreen();
            }
        }
        //anim, sound
    }


}
