using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public int width;
    public int height;

    public int borderSize;

    public GameObject tilePrefab;
    public GameObject[] gamePiecePrefabs;

    Tile[,] m_allTiles;
    GamePiece[,] m_allGamePieces;
    // Start is called before the first frame update
    void Start()
    {
        m_allTiles = new Tile[width, height];
        m_allGamePieces = new GamePiece[width, height];

        SetupTile();
        SetupCamera();
        FillRandom();
    }

    void SetupTile()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                GameObject tile = Instantiate(tilePrefab, new Vector3(i, j, 0), Quaternion.identity) as GameObject;
                tile.name = "Tile (" + i + "," + j + ")";

                m_allTiles[i, j] = tile.GetComponent<Tile>();

                tile.transform.parent = transform;

                m_allTiles[i, j].Init(i, j, this);
            }
        }
    }

    void SetupCamera()
    {
        Camera.main.transform.position = new Vector3((float)(width - 1) / 2f, (float)(height - 1) / 2f, -10f);
        float aspectRatio = (float)Screen.width / (float)Screen.height;
        float verticalSize = (float)height / 2f + (float)borderSize;
        float horizontalSize = ((float)width + (float)borderSize) / 2f / aspectRatio;
        Camera.main.orthographicSize = (verticalSize > horizontalSize) ? verticalSize : horizontalSize;

    }

    GameObject GetRandomPiece()
    {
        int randomIdx = Random.Range(0, gamePiecePrefabs.Length);
        if (gamePiecePrefabs[randomIdx] == null)
        {
            Debug.LogWarning("BOARD: " + randomIdx + "does not contain a valid GamePiece prefab!");
        }
        return gamePiecePrefabs[randomIdx];
    }

    void PlaceGamePiece(GamePiece gamePiece, int x, int y)
    {
        if (gamePiece == null)
        {
            Debug.LogWarning("BOARD: Invalid GamePiece!");
            return;
        }

        gamePiece.transform.position = new Vector3(x, y, 0);
        gamePiece.transform.rotation = Quaternion.identity;
        gamePiece.SetCoord(x, y);

    }

    void FillRandom()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                GameObject randomPiece = Instantiate(GetRandomPiece(), Vector2.zero, Quaternion.identity);

                if (randomPiece != null)
                {
                    randomPiece.name = "GamePiece (" + i + "," + j + ")";
                    PlaceGamePiece(randomPiece.GetComponent<GamePiece>(), i, j);
                }
                
            }
        }
    }

}