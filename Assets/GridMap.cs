using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMap : MonoBehaviour
{
    [SerializeField]
    private int columns = 17;
    [SerializeField]
    private int rows = 9;
    [SerializeField]
    private float cellSizeX = 1.0f;
    [SerializeField]
    private float cellSizeY = 1.0f;
    [SerializeField]
    private Transform gridStart;
    [SerializeField]
    private GameObject stonePrefab;

    // Start is called before the first frame update
    void Start()
    {
        GenerateStartingMap();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GenerateStartingMap()
    {
        Vector2 gridStartPosition = gridStart.position;
        Vector2 gridYdirection = gridStart.up.normalized;
        for (int x = 1; x < columns; x+=2)
        {
            for (int y = 1; y < rows; y+=2)
            {
                Instantiate(stonePrefab, gridStartPosition + new Vector2(x * cellSizeX, 0) + (gridYdirection * cellSizeY * y), Quaternion.identity);
            }
        }
    }
}
