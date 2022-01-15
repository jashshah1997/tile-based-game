using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] 
    private int m_width, m_height;

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
    }

    public Tile GetTileAtPosition(Vector2 pos)
    {
        if (m_tiles.TryGetValue(pos, out var tile)) return tile;
        return null;
    }
}