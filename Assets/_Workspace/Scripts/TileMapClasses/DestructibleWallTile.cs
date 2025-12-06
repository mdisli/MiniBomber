using System;
using _Workspace.Scripts.Walls;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace _Workspace.Scripts.TileMapClasses
{
    [CreateAssetMenu(menuName = "MiniBomber/Destructible Wall"), Serializable]
    public class DestructibleWallTile : Tile
    {
        [Header("Layer Order")]
        public int orderInLayer;
        
        [Header("Prefab")]
        public DestructibleWall wallPrefab;
        
        [Header("Options")]
        [Range(1,3)]public int healthCount;
        
        private void OnValidate()
        {
            var currentColor = color;
            currentColor.a = (float)1 / healthCount; // Duvarı kırmak zorlaştıkça alpha azalır.
            color = currentColor;
        }
        
    }
}