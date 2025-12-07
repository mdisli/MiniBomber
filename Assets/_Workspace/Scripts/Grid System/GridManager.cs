using System;
using _Workspace.Scripts.TileMapClasses;
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
                    case DestructibleWallTile destructibleWallTile:
                    {
                        var destructibleWall = Instantiate(destructibleWallTile.wallPrefab, worldPos, Quaternion.identity,destructibleParent);

                        destructibleWall.Initialize(destructibleWallTile.healthCount, destructibleWallTile.orderInLayer);
                    
                        destructibleTilemap.SetTile(tilePos, null);
                        break;
                    }
                    case RegenerativeWallTile regenerativeWallTile:
                    {
                        RegenerativeWall regenerativeWall = Instantiate(regenerativeWallTile.regenerativeWallPrefab, worldPos, Quaternion.identity, destructibleParent);
                    
                        regenerativeWall.Initialize(regenerativeWallTile.orderInLayer, regenerativeWallTile.healthCount, regenerativeWallTile.regenerateAfter);
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
        #endregion
    }
}