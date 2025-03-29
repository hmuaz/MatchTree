﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public int width;
    public int height;

    public GameObject tilePrefab;

    Tile[,] m_allTiles;
    // Start is called before the first frame update
    void Start()
    {
        m_allTiles = new Tile[width, height];
        SetupTile();
    }

    private void SetupTile()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                GameObject tile = Instantiate(tilePrefab, new Vector3(i, j, 0), Quaternion.identity) as GameObject;
                tile.name = "Tile (" + i + "," + j + ")";

                m_allTiles[i, j] = tile.GetComponent<Tile>();

                tile.transform.parent = transform;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
