using System;
using _Workspace.Scripts.TileMapClasses;
using _Workspace.Scripts.TileMapClasses.Data;
using _Workspace.Scripts.Walls;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace _Workspace.Scripts.Grid_System
{
    public class GridManager : MonoBehaviour
    {
        #region Variables

        [Header("TileMaps")]
        [SerializeField] private Tilemap destructibleTilemap;
        [SerializeField] private Tilemap groundAndWallsTilemap;
        
        [Header("References")]
        [SerializeField] private Transform destructibleParent;
        #endregion

        #region Unity Functions

        private void Start()
        {
            GenerateDestructible();
        }

        #endregion
        
        #region Funcs

        private void GenerateDestructible()
        {
            var bounds = destructibleTilemap.cellBounds;

            var allTiles = destructibleTilemap.GetTilesBlock(bounds);
            for (var i = 0; i < allTiles.Length; i++)
            {
                var tileBase = allTiles[i];
                if (tileBase is null) continue;

                int x = i % bounds.size.x;
                int y = i / bounds.size.x;
                Vector3Int tilePos = new Vector3Int(bounds.xMin + x, bounds.yMin + y, 0);
                Vector3 worldPos = destructibleTilemap.GetCellCenterWorld(tilePos);
                
                switch (tileBase)
                {
                    case WallTile wallTile:
                    {
                        BaseWallData data = wallTile.WallData;
                        
                        BaseWall baseWall = Instantiate(wallTile.WallData.wallPrefab, worldPos, Quaternion.identity,destructibleParent);

                        baseWall.Initialize(data);
                    
                        destructibleTilemap.SetTile(tilePos, null);
                        
                        break;
                    }
                }
            }
        }

        public Vector2 GetTileCenterWithPosition(Vector2 position)
        {
            var tilePosition = groundAndWallsTilemap.WorldToCell(position);
            return groundAndWallsTilemap.GetCellCenterWorld(tilePosition);
        }
        public Bounds GetGridBounds()
        {
            groundAndWallsTilemap.CompressBounds();
            var cellBounds = groundAndWallsTilemap.cellBounds;
            
            var minWorld = groundAndWallsTilemap.CellToWorld(cellBounds.min);
            var maxWorld = groundAndWallsTilemap.CellToWorld(cellBounds.max);

            var center = (minWorld + maxWorld) / 2f;
            var size = maxWorld - minWorld;

            return new Bounds(center, size);
        }
        #endregion
    }
}