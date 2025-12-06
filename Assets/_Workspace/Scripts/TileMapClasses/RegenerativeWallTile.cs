using _Workspace.Scripts.Walls;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace _Workspace.Scripts.TileMapClasses
{
    [CreateAssetMenu(menuName = "MiniBomber/Regenerative Wall")]
    public class RegenerativeWallTile : Tile
    {
        [Header("Layer Order")]
        public int orderInLayer;
        
        [Header("Prefab")]
        public RegenerativeWall regenerativeWallPrefab;
        
        [Header("Options")]
        [Range(1,3)]public int healthCount;
        [Range(3,10)]public float regenerateAfter;
        private void OnValidate()
        {
            float alphaValue = 1f / healthCount; // Can arttıkça şeffaflaşır

            float t = Mathf.InverseLerp(3f, 10f, regenerateAfter);
            
            var targetColor = Color.Lerp(Color.green, Color.red, t); // Regenerate süresi uzadıkça renk kırmızıya döner
            targetColor.a = alphaValue;
            color = targetColor;
        }
    }
}