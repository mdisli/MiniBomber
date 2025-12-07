using System;
using _Workspace.Scripts.Walls;
using UnityEngine;

namespace _Workspace.Scripts.TileMapClasses.Data
{
    [Serializable]
    public class BaseWallData
    {
        [Header("Layer Order")]
        public int orderInLayer;
        
        [Header("Prefab")]
        public BaseWall wallPrefab;
        
        [Header("Options")]
        [Range(1,3)]public int healthCount;
        
        [Header("Regeneration")]
        [Range(3,10)]public float regenerateAfter;
    }
}