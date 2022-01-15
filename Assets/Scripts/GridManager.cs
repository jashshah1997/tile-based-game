using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GridManager : MonoBehaviour
{

    [SerializeField]
    private int m_width, m_height, m_maxGoldPiles;

    [SerializeField] private Tile m_tilePrefab;

    [SerializeField] private Transform m_cam;

    private Dictionary<Vector2, Tile> m_tiles;

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        m_tiles = new Dictionary<Vector2, Tile>();
        for (int x = 0; x < m_width; x++)
        {
            for (int y = 0; y < m_height; y++)
            {
                var spawnedTile = Instantiate(m_tilePrefab, new Vector3(x, y), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";

                var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                spawnedTile.Init(isOffset);


                m_tiles[new Vector2(x, y)] = spawnedTile;
            }
        }

        m_cam.transform.position = new Vector3((float)m_width - 2, (float)m_height / 2 - 0.5f, -10);

        int generatedPiles = 0;
        int failedCounter = 10;
        while (generatedPiles < m_maxGoldPiles && failedCounter > 0)
        {
            int x_rand = UnityEngine.Random.Range(2, m_width - 2);
            int y_rand = UnityEngine.Random.Range(2, m_height - 2);
            Vector2 key = new Vector2(x_rand, y_rand);

            // Check if tile is occupied
            if (isGoldNearby(key))
            {
                failedCounter--;
                continue;
            }

            failedCounter = 10;
            int goldAmount = UnityEngine.Random.Range(2000, 5000);

            m_tiles[key].setGoldStatus(GoldStatus.FULL);
            m_tiles[key].setGoldAmount(goldAmount);
            putGoldOnTiles(key, goldAmount);
            generatedPiles++;
        }
    }
    
    bool isGoldNearby(Vector2 index)
    {
        int x = (int)index.x;
        int y = (int)index.y;
        for (int off_x = -2; off_x <= 2; off_x++)
        {
            for (int off_y = -2; off_y <= 2; off_y++)
            {
                Vector2 key = new Vector2(x + off_x, y + off_y);
                Debug.Log(key); 
                if (m_tiles[key].isOccupied()) return true;
            }
        }
        return false;
    }

    void putGoldOnTiles(Vector2 index, int goldAmount)
    {
        int x = (int)index.x;
        int y = (int)index.y;
        for (int off_x = - 2; off_x <= 2; off_x++)
        {
            for (int off_y = - 2; off_y <= 2; off_y++)
            {
                Vector2 key = new Vector2(x + off_x, y + off_y);

                // Central tile
                if (off_x == 0 && off_y == 0)
                {
                    continue;
                }

                if (Math.Abs(off_x) + Math.Abs(off_y) <= 2 && 
                    Math.Abs(off_x) != 2 && Math.Abs(off_y) != 2)
                {
                    m_tiles[key].setGoldStatus(GoldStatus.HALF);
                    m_tiles[key].setGoldAmount(goldAmount);
                }
                else
                {
                    m_tiles[key].setGoldStatus(GoldStatus.QUARTER);
                    m_tiles[key].setGoldAmount(goldAmount);
                }
            }
        }
    }

    public Tile GetTileAtPosition(Vector2 pos)
    {
        if (m_tiles.TryGetValue(pos, out var tile)) return tile;
        return null;
    }
}