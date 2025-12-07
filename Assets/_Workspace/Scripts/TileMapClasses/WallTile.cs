using _Workspace.Scripts.TileMapClasses.Data;
using _Workspace.Scripts.Walls;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace _Workspace.Scripts.TileMapClasses
{
    [CreateAssetMenu(fileName = "WallTile", menuName = "So/WallTile", order = 0)]
    public class WallTile : Tile
    {
        public BaseWallData WallData;
        
        private void OnValidate()
        {
            float alphaValue = 1f / WallData.healthCount; // Can arttıkça şeffaflaşır
            var currentColor = color;
            currentColor.a = alphaValue;
            color = currentColor;

            RegenerativeWall regenWall  = WallData.wallPrefab as RegenerativeWall;
            
            if(regenWall is null) return;
            float t = Mathf.InverseLerp(3f, 10f, WallData.regenerateAfter);
            
            currentColor = Color.Lerp(Color.yellow, Color.green, t); // Regenerate süresi uzadıkça renk kırmızıya döner
            currentColor.a = alphaValue;
            color = currentColor;
        }
    }
}