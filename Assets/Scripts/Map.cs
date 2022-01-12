using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Map : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private int columns = 17;
    [SerializeField]
    private int rows = 9;
    [SerializeField]
    private float hayFrequency;
    [SerializeField]
    private float cellSizeX = 1.0f;
    [SerializeField]
    private float cellSizeY = 1.0f;
    [SerializeField]
    private Transform gridStart;
    [SerializeField]
    private GameObject stonePrefab;
    [SerializeField]
    private GameObject hayPrefab;


    // Start is called before the first frame update
    public override void OnJoinedRoom()
    {
        GenerateStartingMap();
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    private void GenerateStartingMap()
    {
        if (!PhotonNetwork.IsMasterClient)//only master client builds map
        {
            return;
        }

        System.Random randFloat = new System.Random();
        Vector2 gridStartPosition = gridStart.position;
        Vector2 gridYdirection = gridStart.up.normalized;
        for (int x = 1; x < columns; x++)
        {
            for (int y = 1; y < rows; y++)
            {
                if (x % 2 == 1 && y % 2 == 1)
                {
                    PhotonNetwork.Instantiate(stonePrefab.name, gridStartPosition + new Vector2(x * cellSizeX, 0) + (gridYdirection * cellSizeY * y), Quaternion.identity);
                }
                else
                {
                    if (randFloat.NextDouble() < hayFrequency)
                    {
                        PhotonNetwork.Instantiate(hayPrefab.name, gridStartPosition + new Vector2(x * cellSizeX, 0) + (gridYdirection * cellSizeY * y), Quaternion.identity);
                    }
                }
                
            }
        }
    }
}
